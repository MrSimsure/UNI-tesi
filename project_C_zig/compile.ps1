zig cc ./main_draw_50K.c -I ".\SDL2\include" -L ".\SDL2\lib" -lSDL2main -lSDL2 -o "./dist/main_draw_50K.exe" -Ofast
zig cc ./main_code_50K.c -I ".\SDL2\include" -L ".\SDL2\lib" -lSDL2main -lSDL2 -o "./dist/main_code_50K.exe" -Ofast
zig cc ./main_code_500K.c -I ".\SDL2\include" -L ".\SDL2\lib" -lSDL2main -lSDL2 -o "./dist/main_code_500K.exe" -Ofast