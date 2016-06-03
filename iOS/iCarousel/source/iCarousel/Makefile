top_srcdir=../../../..
TOP=$(top_srcdir)

include $(top_srcdir)/commands.make

PROJECTNAME=iCarousel
SLNFILE=../iCarousel.sln
PROJECTNAME_CLASSIC=iCarousel-Classic
SLNFILE_CLASSIC=../iCarousel.sln

BUILDCONFIGURATION=Release
BUILDOUTPUTDIR = bin/unified/$(BUILDCONFIGURATION)
BUILDOUTPUTDIR_CLASSIC = bin/classic/$(BUILDCONFIGURATION)

ASSEMBLYFILE=$(PROJECTNAME).dll
BUILDOUTPUT = $(BUILDOUTPUTDIR)/$(ASSEMBLYFILE)
ASSEMBLYFILE_CLASSIC=$(PROJECTNAME).dll
BUILDOUTPUT_CLASSIC = $(BUILDOUTPUTDIR_CLASSIC)/$(ASSEMBLYFILE)


all: $(BUILDOUTPUT) $(BUILDOUTPUT_CLASSIC)

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
