set -e

rm -f package.zip
zip --junk-paths package.zip README.md manifest.json icon.png CHANGELOG.md ./AutoAlarm/bin/Debug/netstandard2.0/AutoAlarm.dll
