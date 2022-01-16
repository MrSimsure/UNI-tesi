particles = 500000
list = array_create(particles)

function Particle() constructor  
{
	xx = 0;
	yy = 0;
	origin_x = 0;
	origin_y = 0;
	w = 10;
	h = 10;
	angle = 0;
}


for(var i=0; i<particles; i++)
{
	var par = new Particle()
	par.xx = random_range(0, 960)
	par.yy = random_range(0, 540)
	par.origin_x = par.xx
	par.origin_y = par.yy
	par.angle = random_range(0,360)
	
	list[i] = par
}