using System;
using SDL_Sharp;
using SDL_Sharp.Image;

namespace TestConfronto
{
    public struct Particle
    {
        public Rect rect;     
        public int origin_x;
        public int origin_y;
        public int angle;
    }

    class Program
    {
        const int  WIDTH = 960;
        const int  HEIGHT = 540;
        const int  SIZE = 10;
        const int  FPS = 60;

        const int  PARTICLES = 50000;


        static int random_range(int minimum_number, int max_number)
        {
            return new Random().Next(minimum_number, max_number);
        }
        
        unsafe static int Main()
        { 
            if (SDL.Init(SdlInitFlags.Everything) != 0)
            {
                Console.WriteLine("Error initializing SDL: %s\n" );
                return 0;
            }
            
            IntPtr wind = SDL.CreateWindow("Thesis - 50K draw C#",SDL.WINDOWPOS_CENTERED,SDL.WINDOWPOS_CENTERED,WIDTH, HEIGHT, 0);
            if (wind == IntPtr.Zero)
            {
                Console.WriteLine("Error creating window: %s\n");
                SDL.Quit();
                return 0;
            }

            
            RendererFlags render_flags = RendererFlags.Accelerated | RendererFlags.PresentVsync;
            IntPtr rend = SDL.CreateRenderer(wind, -1, render_flags);
            if (wind == IntPtr.Zero)
            {
                Console.WriteLine("Error creating renderer: %s\n");
                SDL.DestroyWindow(wind);
                SDL.Quit();
                IMG.Quit();
                return 0;
            }
  

            Color col = new Color();
            col.R = 255;
            col.G = 255;
            col.B = 255;
            col.A = 255;


            bool running = true;
            Event mainEvent; 
            int numFrames = 0;
            uint startTime  = SDL.GetTicks();

            Particle[] entity = new Particle[PARTICLES];
            for(int i=0; i<PARTICLES; i++)
            {    
                Particle particle = new();

                particle.rect = new();
                particle.rect.X = random_range(0,WIDTH);
                particle.rect.Y = random_range(0,HEIGHT);
                particle.rect.Width = SIZE;
                particle.rect.Height = SIZE; 

                particle.origin_x = particle.rect.X;
                particle.origin_y = particle.rect.Y;
                particle.angle = random_range(0,360);

                entity[i] = particle;
            }


            while (running)
            {     
                ++numFrames;
                uint elapsedMS = SDL.GetTicks() - startTime;
                if(elapsedMS > 1) 
                { 
                  double elapsedSeconds = elapsedMS / 1000.0; 
                  double fps = numFrames / elapsedSeconds; 
                  fps = Math.Round(fps);
                  Console.WriteLine("FPS = "+fps);
                }


                while (Convert.ToBoolean(SDL.PollEvent(out mainEvent)))
                {
                    switch (mainEvent.Type)
                    {
                        case EventType.Quit:
                        {
                            running = false;
                            break;
                        }   
                    }
                }
	
                SDL.SetRenderDrawColor(rend, 0, 0, 0, 255);
                SDL.RenderClear(rend);

                SDL.SetRenderDrawColor(rend, 255, 0, 0, 255);
                for(int i=0; i<PARTICLES; i++)
                {    
                    Particle particle = entity[i];

                    particle.angle += 1;

                    particle.rect.X = (int)(particle.origin_x + Math.Cos(particle.angle* Math.PI / 180)*100);
                    particle.rect.Y = (int)(particle.origin_y + Math.Sin(particle.angle* Math.PI / 180)*100);
                    
                    entity[i] = particle;

                    SDL.RenderDrawRect(rend, ref particle.rect);
                }

                SDL.RenderPresent(rend);
                SDL.Delay(1/FPS);
            }


            SDL.DestroyRenderer(rend);
            SDL.DestroyWindow(wind);
            SDL.Quit();

            return 0;

        }
    }
}