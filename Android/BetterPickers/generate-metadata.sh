#!/bin/bash

METADATAGEN=../../Tools/class-parse-metadata-generator/ClassParseMetadataNameGenerator.exe

mono $METADATAGEN -ignore=.metadataignore externals/BetterPickers/classes.jar > ./source/BetterPickers/Transforms/Metadata.generated.xml
