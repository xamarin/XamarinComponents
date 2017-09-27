COMMIT="master"
URL="https://raw.githubusercontent.com/xamarin/XamarinComponents/$COMMIT/Util/Bootstrapper/cake.sh"
SCRIPT="./cake.sh"

curl -Lsfo $SCRIPT $URL

if [ $? -ne 0 ]; then
    echo "An error occured while downloading $SCRIPT."
    exit 1
fi

sh $SCRIPT $@
