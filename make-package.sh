set -e

rm -f package.zip /tmp/AutoAlarm.dll
cp ./AutoAlarm/bin/Debug/netstandard2.0/com.geeksville.dsp.AutoAlarm.dll /tmp/AutoAlarm.dll
zip --junk-paths package.zip README.md manifest.json icon.png CHANGELOG.md /tmp/AutoAlarm.dll 
