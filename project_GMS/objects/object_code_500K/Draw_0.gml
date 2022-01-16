draw_set_color(c_red)

for(var i=0; i<particles; i++)
{
	var o = list[i]
	
	o.angle += 1;
	o.xx = o.origin_x + cos(o.angle* pi / 180.0)*100;
	o.yy = o.origin_y + sin(o.angle* pi / 180.0)*100;
}

draw_set_color(c_white)