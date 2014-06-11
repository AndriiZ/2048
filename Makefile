all:
	xbuild /p:Configuration=Release  2048.csproj

debug:
	xbuild /p:Configuration=Debug  2048.csproj
