top_srcdir=../../../../..
TOP=$(top_srcdir)

include $(top_srcdir)/commands.make

PROJECTNAME=BasiciOSExample
SLNFILE=../iCarouselSamples.sln
PROJECTNAME_CLASSIC=BasiciOSExample-Classic
SLNFILE_CLASSIC=../iCarouselSamples-Classic.sln

BUILDCONFIGURATION=Release|iPhone
BUILDCONFIGURATIONDIR=iPhone/Release
BUILDOUTPUTDIR = bin/unified/$(BUILDCONFIGURATIONDIR)
BUILDOUTPUTDIR_CLASSIC = bin/classic/$(BUILDCONFIGURATIONDIR)

ASSEMBLYFILE=BasiciOSExample.exe
BUILDOUTPUT = $(BUILDOUTPUTDIR)/$(ASSEMBLYFILE)
ASSEMBLYFILE_CLASSIC=BasiciOSExampleClassic.exe
BUILDOUTPUT_CLASSIC = $(BUILDOUTPUTDIR_CLASSIC)/$(ASSEMBLYFILE_CLASSIC)


# NOTE TO DEV:
# be sure to add the classic back into the build after the fix

all: $(BUILDOUTPUT) # $(BUILDOUTPUT_CLASSIC)

$(BUILDOUTPUT) : $(shell find . -type f -name '*.cs' -not -path "./obj/*" -not -path "") \
	$(shell find . -type f -name '*.xml' -not -path "./obj/*" -not -path "./bin/*") \
        $(PROJECTNAME).csproj

	$(MDTOOL) -v build "--configuration:$(BUILDCONFIGURATION)" $(SLNFILE) -p:$(PROJECTNAME)

$(BUILDOUTPUT_CLASSIC) : $(shell find . -type f -name '*.cs' -not -path "./obj/*" -not -path "") \
	$(shell find . -type f -name '*.xml' -not -path "./obj/*" -not -path "./bin/*") \
        $(PROJECTNAME_CLASSIC).csproj

	$(MDTOOL) -v build "--configuration:$(BUILDCONFIGURATION)" $(SLNFILE_CLASSIC) -p:$(PROJECTNAME_CLASSIC)

clean:
	rm -rf bin/ obj/ 


.PHONY : all clean
