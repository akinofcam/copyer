#!/bin/bash

# ============================================================================
# Copyer - Professional Directory Cloning Tool
# Installation Script for macOS and Linux
# ============================================================================

# Colors for output
RED='\033[0;31m'
GREEN='\033[0;32m'
YELLOW='\033[1;33m'
CYAN='\033[0;36m'
BOLD='\033[1m'
NC='\033[0m' # No Color

# Functions
print_header() {
    echo -e "${CYAN}╔════════════════════════════════════════════════════════════╗${NC}"
    echo -e "${CYAN}║          Welcome to Copyer Installation Wizard              ║${NC}"
    echo -e "${CYAN}║     Professional Directory Cloning Tool for Unix Systems    ║${NC}"
    echo -e "${CYAN}╚════════════════════════════════════════════════════════════╝${NC}"
    echo ""
}

print_error() {
    echo -e "${RED}❌ $1${NC}"
}

print_success() {
    echo -e "${GREEN}✅ $1${NC}"
}

print_warning() {
    echo -e "${YELLOW}⚠️  $1${NC}"
}

print_info() {
    echo -e "${CYAN}ℹ️  $1${NC}"
}

# Greeting and System Detection
print_header

echo -e "${YELLOW}🔍 System Detection:${NC}"

# Detect OS
OS="Unknown"
ARCH=$(uname -m)

case "$(uname -s)" in
    Linux*)
        OS="Linux"
        DISTRO=$(lsb_release -ds 2>/dev/null || echo "Linux")
        ;;
    Darwin*)
        OS="macOS"
        DISTRO="macOS $(sw_vers -productVersion)"
        ;;
    *)
        OS="Unknown"
        ;;
esac

echo "   OS: ${GREEN}$DISTRO${NC}"
echo "   Architecture: ${GREEN}$ARCH${NC}"

# Detect architecture for downloads
case "$ARCH" in
    x86_64)
        DOWNLOAD_ARCH="x64"
        ;;
    arm64|aarch64)
        DOWNLOAD_ARCH="arm64"
        ;;
    *)
        print_error "Unsupported architecture: $ARCH"
        exit 1
        ;;
esac

# Check for .NET installation
if command -v dotnet &> /dev/null; then
    DOTNET_VERSION=$(dotnet --version)
    echo "   .NET Version: ${GREEN}$DOTNET_VERSION${NC}"
else
    echo ""
    print_error ".NET SDK is not installed!"
    echo -e "${YELLOW}   Please install .NET 10.0 or later:${NC}"
    echo -e "${CYAN}   • macOS: brew install dotnet${NC}"
    echo -e "${CYAN}   • Linux: Visit https://dotnet.microsoft.com/download${NC}"
    exit 1
fi

echo ""
print_success ".NET SDK found - proceeding with installation"
echo ""

# Download and Install
echo -e "${YELLOW}📥 Downloading Copyer...${NC}"

INSTALL_DIR="$HOME/.copyer"
DOWNLOAD_URL=""

# Get latest release info
REPO_API="https://api.github.com/repos/akinofcam/copyer/releases/latest"

# Check internet connection
if ! command -v curl &> /dev/null; then
    print_error "curl is required but not installed"
    exit 1
fi

# Fetch release info
RELEASE_INFO=$(curl -s "$REPO_API")
TAG=$(echo "$RELEASE_INFO" | grep -o '"tag_name": "[^"]*' | head -1 | cut -d'"' -f4)
VERSION="${TAG#v}"

if [ -z "$TAG" ]; then
    print_error "Failed to fetch latest release from GitHub"
    echo "   Please check your internet connection and try again"
    exit 1
fi

echo "   Release: ${GREEN}$TAG${NC}"

# Try to find appropriate asset
if [[ "$OS" == "macOS" ]]; then
    ASSET_NAME="copyer-arm64.zip"
    if [ "$ARCH" == "x86_64" ]; then
        ASSET_NAME="copyer-x64.zip"
    fi
else
    ASSET_NAME="copyer-$DOWNLOAD_ARCH.tar.gz"
fi

# Extract download URL
DOWNLOAD_URL=$(echo "$RELEASE_INFO" | grep -o "\"browser_download_url\": \"[^\"]*$ASSET_NAME" | head -1 | cut -d'"' -f4)

if [ -z "$DOWNLOAD_URL" ]; then
    # Fallback to first available asset
    DOWNLOAD_URL=$(echo "$RELEASE_INFO" | grep -o '"browser_download_url": "[^"]*' | head -1 | cut -d'"' -f4)
fi

if [ -z "$DOWNLOAD_URL" ]; then
    print_error "No release assets found"
    exit 1
fi

echo "   Downloading from: ${CYAN}$DOWNLOAD_URL${NC}"

# Create install directory
mkdir -p "$INSTALL_DIR"

# Download file
TEMP_FILE="/tmp/copyer-download"
if ! curl -L -o "$TEMP_FILE" "$DOWNLOAD_URL" 2>/dev/null; then
    print_error "Failed to download Copyer"
    exit 1
fi

print_success "Downloaded successfully"

# Extract archive
echo ""
echo -e "${YELLOW}📦 Installing to: $INSTALL_DIR${NC}"

if [[ "$ASSET_NAME" == *.zip ]]; then
    if ! unzip -q "$TEMP_FILE" -d "$INSTALL_DIR"; then
        print_error "Failed to extract archive"
        exit 1
    fi
elif [[ "$ASSET_NAME" == *.tar.gz ]]; then
    if ! tar -xzf "$TEMP_FILE" -C "$INSTALL_DIR"; then
        print_error "Failed to extract archive"
        exit 1
    fi
fi

# Make executable
chmod +x "$INSTALL_DIR/Copyer" 2>/dev/null || chmod +x "$INSTALL_DIR/copyer" 2>/dev/null

# Create symlink for command
SYMLINK_PATH="/usr/local/bin/copy"
ACTUAL_BIN=""

if [ -f "$INSTALL_DIR/Copyer" ]; then
    ACTUAL_BIN="$INSTALL_DIR/Copyer"
elif [ -f "$INSTALL_DIR/copyer" ]; then
    ACTUAL_BIN="$INSTALL_DIR/copyer"
fi

if [ -z "$ACTUAL_BIN" ]; then
    print_error "Could not find Copyer executable in installed files"
    exit 1
fi

# Try to create symlink (may require sudo)
if sudo -n test 2>/dev/null; then
    sudo ln -sf "$ACTUAL_BIN" "$SYMLINK_PATH" 2>/dev/null
    if [ -L "$SYMLINK_PATH" ]; then
        print_success "Symlink created at $SYMLINK_PATH"
    fi
elif [ -w /usr/local/bin ]; then
    ln -sf "$ACTUAL_BIN" "$SYMLINK_PATH"
    print_success "Symlink created at $SYMLINK_PATH"
else
    echo -e "${YELLOW}   Could not create system symlink (requires sudo)${NC}"
    echo -e "${CYAN}   You can run Copyer from: $ACTUAL_BIN${NC}"
fi

# Add to PATH if needed
if ! grep -q "\.copyer" ~/.bashrc ~/.zshrc 2>/dev/null; then
    if [ -n "$ZSH_VERSION" ] && [ -f ~/.zshrc ]; then
        echo 'export PATH="$HOME/.copyer:$PATH"' >> ~/.zshrc
        print_success "Added .copyer to ~/.zshrc"
    elif [ -f ~/.bashrc ]; then
        echo 'export PATH="$HOME/.copyer:$PATH"' >> ~/.bashrc
        print_success "Added .copyer to ~/.bashrc"
    fi
fi

# Cleanup
rm -f "$TEMP_FILE"

echo ""
print_success "Installation complete!"
echo ""

# Test installation
echo -e "${YELLOW}🧪 Testing installation...${NC}"

if command -v copy &> /dev/null; then
    print_success "Command 'copy' is available globally"
elif [ -x "$ACTUAL_BIN" ]; then
    print_success "Copyer executable is ready at $ACTUAL_BIN"
fi

echo ""

# Ask if user wants to run
read -p "$(echo -e ${CYAN}📋 Would you like to run Copyer now? (y/n): ${NC})" -n 1 -r
echo ""

if [[ $REPLY =~ ^[Yy]$ ]]; then
    echo ""
    echo -e "${CYAN}📂 Enter the source directory to clone:${NC}"
    read -p "   Source Directory: " SOURCE_DIR
    
    if [ ! -d "$SOURCE_DIR" ]; then
        print_error "Directory not found: $SOURCE_DIR"
        exit 1
    fi
    
    echo ""
    echo -e "${CYAN}📂 Enter the destination (leave empty for current directory):${NC}"
    read -p "   Destination Directory: " DEST_DIR
    
    echo ""
    echo -e "${CYAN}🚀 Running Copyer...${NC}"
    echo ""
    
    if [ -z "$DEST_DIR" ]; then
        "$ACTUAL_BIN" "$SOURCE_DIR"
    else
        "$ACTUAL_BIN" "$SOURCE_DIR" -d "$DEST_DIR"
    fi
    
    echo ""
fi

echo -e "${YELLOW}📚 Next Steps:${NC}"
echo -e "${CYAN}   • Run: copy --help              (show all options)${NC}"
echo -e "${CYAN}   • Usage: copy /path/to/source  (clone directory)${NC}"
echo -e "${CYAN}   • Verify: copy /path -v        (verify copied files)${NC}"
echo -e "${CYAN}   • Docs: https://github.com/akinofcam/copyer${NC}"
echo ""
echo -e "${GREEN}✨ Installation wizard completed!${NC}"
echo ""
