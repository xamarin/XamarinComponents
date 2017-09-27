COMMIT="443e9b8c6efb624177cdc0cea3c750e4bfff1910"
URL="https://raw.githubusercontent.com/xamarin/XamarinComponents/$COMMIT/Util/Bootstrapper/cake.sh"
SCRIPT_DIR=$( cd "$( dirname "${BASH_SOURCE[0]}" )" && pwd )
SCRIPT="$SCRIPT_DIR/cake.sh"

# Get the commit last used to grab these files so we can check if an update's needed
commitTxt=""    
if [ -f "$SCRIPT.commit" ]; then commitTxt=$(cat "$SCRIPT.commit"); fi

# If the cached file's commit doesn't match our desired, or if it's 'master' go download the right version
if [[ "$commitTxt" != "$COMMIT" ||  "$commitTxt" == "master" ]]; then

    echo "Downloading $SCRIPT..."

    # Download the script
    curl -Lsfo $SCRIPT $URL
    if [ $? -ne 0 ]; then
        echo "An error occured while downloading $SCRIPT."
        exit 1
    fi

    # Write out the commit hash used to cache for future runs
    echo $COMMIT > "$SCRIPT.commit"
fi

export BOOTSTRAPPER_COMMIT="$COMMIT"
sh $SCRIPT $@
