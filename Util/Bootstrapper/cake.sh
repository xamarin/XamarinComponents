#!/usr/bin/env bash

##########################################################################
# Xamarin Custom bootstrap script for Cake
##########################################################################

# Define directories.
SCRIPT_DIR=$( cd "$( dirname "${BASH_SOURCE[0]}" )" && pwd )
TOOLS_DIR=$SCRIPT_DIR/tools
ADDINS_DIR=$TOOLS_DIR/Addins
MODULES_DIR=$TOOLS_DIR/Modules
NUGET_EXE=$TOOLS_DIR/nuget.exe
CAKE_EXE=$TOOLS_DIR/Cake/Cake.exe
PACKAGES_CONFIG=$TOOLS_DIR/packages.config
PACKAGES_CONFIG_MD5=$TOOLS_DIR/packages.config.md5sum
ADDINS_PACKAGES_CONFIG=$ADDINS_DIR/packages.config
MODULES_PACKAGES_CONFIG=$MODULES_DIR/packages.config

# Define md5sum or md5 depending on Linux/OSX
MD5_EXE=
if [[ "$(uname -s)" == "Darwin" ]]; then
    MD5_EXE="md5 -r"
else
    MD5_EXE="md5sum"
fi

# Define default arguments.
SCRIPT="./build.cake"
if [ -z "$BOOTSTRAPPER_COMMIT" ]; then
    BOOTSTRAPPER_COMMIT="master"
fi

CAKE_ARGUMENTS=()

# Parse arguments.
for i in "$@"; do
    case $1 in
        -s|--script) SCRIPT="$2"; shift ;;
        --) shift; CAKE_ARGUMENTS+=("$@"); break ;;
        *) CAKE_ARGUMENTS+=("$1") ;;
    esac
    shift
done

BOOTSTRAPPER_URL_BASE="https://raw.githubusercontent.com/xamarin/XamarinComponents/$BOOTSTRAPPER_COMMIT/Util/Bootstrapper"

# Define files to download locally
script_files=( '/tools/packages.config' '/tools/addins.cake' )

# Loop through all the files and make sure they are up to date and download if necessary
for f in "${script_files[@]}"
do
	#fileUrl = "${script_files[$localFile]}"
    fileUrl="$BOOTSTRAPPER_URL_BASE$f"
    localFile="$SCRIPT_DIR$f"
    fileCommitFile="$localFile.commit"

    # Make sure the directory for the destination file exists
    mkdir -p $(dirname "$localFile")

    # Get the commit last used to grab these files so we can check if an update's needed
    fileCommitContent=""    
    if [ -f $fileCommitFile ]; then
        fileCommitContent=$(cat "$fileCommitFile")
    fi

    # If the cached file's commit doesn't match our desired, or if it's 'master' go download the right version
    if [[ "$fileCommitContent" != "$BOOTSTRAPPER_COMMIT" ||  "$fileCommitContent" == "master" ]]; then
        echo "Downloading $localFile... $fileUrl"
        
        # Download the file
        curl -Lsfo $localFile $fileUrl
        if [ $? -ne 0 ]; then
            echo "An error occured while downloading $localFile."
            exit 1
        fi

        # Write out the commit hash used to cache for future runs
        echo $BOOTSTRAPPER_COMMIT > $fileCommitFile
    fi
done

# Download NuGet if it does not exist.
if [ ! -f "$NUGET_EXE" ]; then
   echo "Downloading NuGet..."
   curl -Lsfo "$NUGET_EXE" https://dist.nuget.org/win-x86-commandline/latest/nuget.exe
   if [ $? -ne 0 ]; then
       echo "An error occured while downloading nuget.exe."
       exit 1
   fi
fi

# Restore tools from NuGet.
pushd "$TOOLS_DIR" >/dev/null
mono "$NUGET_EXE" install -ExcludeVersion
if [ $? -ne 0 ]; then
   echo "Could not restore NuGet tools."
   exit 1
fi
popd >/dev/null

# Restore addins from NuGet.
if [ -f "$ADDINS_PACKAGES_CONFIG" ]; then
    pushd "$ADDINS_DIR" >/dev/null

   mono "$NUGET_EXE" install -ExcludeVersion
   if [ $? -ne 0 ]; then
       echo "Could not restore NuGet addins."
       exit 1
   fi

   popd >/dev/null
fi

# Restore modules from NuGet.
if [ -f "$MODULES_PACKAGES_CONFIG" ]; then
   pushd "$MODULES_DIR" >/dev/null

   mono "$NUGET_EXE" install -ExcludeVersion
   if [ $? -ne 0 ]; then
       echo "Could not restore NuGet modules."
       exit 1
   fi

   popd >/dev/null
fi

# Make sure that Cake has been installed.
if [ ! -f "$CAKE_EXE" ]; then
   echo "Could not find Cake.exe at '$CAKE_EXE'."
   exit 1
fi

# Start Cake
exec mono "$CAKE_EXE" $SCRIPT --nuget_loaddependencies=true --nuget_useinprocessclient=true --settings_skipverification=true "${CAKE_ARGUMENTS[@]}"
