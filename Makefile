all:
	xbuild /p:Configuration=Release  2048.sln

debug:
	xbuild /p:Configuration=Debug  2048.sln
