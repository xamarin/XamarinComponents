
Device Year Class is an Android library that implements a simple algorithm that maps
a device's RAM, CPU cores, and clock speed to the year where those combination
of specs were considered high end. This allows a developer to easily modify
application behavior based on the capabilities of the phone's hardware.

Mappings as of this writing (ceilings, aside from the final row):

|Year|  Cores|  Clock |  RAM  |
|---:|------:|-------:|------:|
|2008|  1    |  528MHz|  192MB|
|2009|  n/a  |  600MHz|  290MB|
|2010|  n/a  |  1.0GHz|  512MB|
|2011|  2    |  1.2GHz|    1GB|
|2012|  4    |  1.5GHz|  1.5GB|
|2013|  n/a  |  2.0GHz|    2GB|
|2014|  n/a  |   >2GHz|   >2GB|


## Usage

Calculating the current device's Year Class is simple:

    var year = YearClass.GetDeviceYearClass(ApplicationContext);

As the operation may take some timne, it is best to do it on a background thread:

    var year = await Task.Run(() => YearClass.GetDeviceYearClass(ApplicationContext));

Then, later on, we can use the year class to make decisions in our app, or
send it along with analytics.

    if (year >= 2013) {
        // Do advanced animation
    } else if (year > 2010) {
        // Do simple animation
    } else {
        // Phone too slow, don't do any animations
    }
