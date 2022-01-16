#include <stdio.h>
#include <stdbool.h>
#include <SDL2/SDL.h>
#include <windows.h>
#include <math.h>

#define WIDTH 960
#define HEIGHT 540
#define SIZE 10
#define SPEED 600
#define GRAVITY 60
#define FPS 60
#define JUMP -1200

#define PARTICLES 500000

typedef struct Particle 
{
   SDL_Rect rect;
   int origin_x;
   int origin_y;
   int angle;
} Particle;

int random_range(int minimum_number, int max_number)
{
  return (rand() % (max_number + 1 - minimum_number) + minimum_number);
}


int WinMain()
{

  if(SDL_Init(SDL_INIT_EVERYTHING) != 0)
  {
      printf("Error initializing SDL: %s\n", SDL_GetError());
      return 0;
  }


  SDL_Window* wind = SDL_CreateWindow("Confrontation Test - C zig",SDL_WINDOWPOS_CENTERED,SDL_WINDOWPOS_CENTERED,WIDTH, HEIGHT, 0);
  if(!wind)
  {
      printf("Error creating window: %s\n", SDL_GetError());
      SDL_Quit();
      return 0;
  }
  

  Uint32 render_flags = SDL_RENDERER_ACCELERATED | SDL_RENDERER_PRESENTVSYNC;
  SDL_Renderer* rend = SDL_CreateRenderer(wind, -1, render_flags);
  if(!rend)
  {
      printf("Error creating renderer: %s\n", SDL_GetError());
      SDL_DestroyWindow(wind);
      SDL_Quit();
      return 0;
  }
  

  bool running = true;
  SDL_Event event;
  int numFrames = 0;
  Uint32 startTime = SDL_GetTicks();

  Particle entity[PARTICLES];
  for(int i=0; i<PARTICLES; i++)
  {    
      Particle* part = &entity[i];
      part->rect.x = random_range(0,WIDTH);
      part->rect.y = random_range(0,HEIGHT);
      part->rect.w = SIZE;
      part->rect.h = SIZE; 

      part->origin_x = part->rect.x;
      part->origin_y = part->rect.y;
      part->angle = random_range(0,360);
  }

  while (running)
  {
    ++numFrames;
    Uint32 elapsedMS = SDL_GetTicks() - startTime; 
    if (elapsedMS) 
    {
      double elapsedSeconds = elapsedMS / 1000.0; 
      double fps = numFrames / elapsedSeconds; 
      printf("FPS = %f\n", fps);
    }


    while (SDL_PollEvent(&event))
    {
        switch (event.type)
        {
          case SDL_QUIT:
          {
              running = false;
              break;
          }
        }
    }
	

    SDL_SetRenderDrawColor(rend, 0, 0, 0, 255);
    SDL_RenderClear(rend);


    SDL_SetRenderDrawColor(rend, 255, 0, 0, 255);
    for(int i=0; i<PARTICLES; i++)
    {    
        Particle* part = &entity[i];
        part->angle += 1;
        part->rect.x = part->origin_x + cos(part->angle* M_PI / 180.0)*100;
        part->rect.y = part->origin_y + sin(part->angle* M_PI / 180.0)*100;
    }
   

    SDL_RenderPresent(rend);
    SDL_Delay(1/FPS);
  }

  SDL_DestroyRenderer(rend);
  SDL_DestroyWindow(wind);
  SDL_Quit();

  return 0;
}

