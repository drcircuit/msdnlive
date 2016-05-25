var canvas = document.createElement("canvas");
canvas.width = window.innerWidth;
canvas.height = window.innerHeight;
document.body.appendChild(canvas);


var ctx = canvas.getContext("2d");


var tree = new Branch(100);

animate();

function animate()
{
    ctx.save();
    ctx.clearRect(0, 0, canvas.width, canvas.height);
    ctx.translate(canvas.width/2, canvas.height - 100);
    
    tree.render();
    
    window.requestAnimationFrame(animate);
    ctx.restore();
}

function Branch(size, rotation)
{
    this.children = [];
    this.sway  = 0;
    this.swaySpeed = random(0.02, 0.2);
    
    if(size>15){
        this.children.push(new Branch(size * random(0.7, 0.9), random(15, 30)));
        this.children.push(new Branch(size * random(0.7, 0.9), random(-15, -30)));
    }
    
    this.render = function()
    {
        ctx.save();
        
        ctx.rotate((rotation  + Math.sin(this.sway)) * Math.PI/180);
        ctx.beginPath();
        ctx.lineWidth = size * 0.1;
        ctx.strokeStyle = 'White';
        ctx.moveTo(0, 0);
        ctx.lineTo(0, -size);

        ctx.stroke();
        ctx.translate(0, -size);
        
        for(var i = 0; i < this.children.length; i++){
            this.children[i].render();
        }
        this.sway += this.swaySpeed;
        ctx.restore();
        
        
    }
    
}

function random(max, min){
    return Math.random() * (max - min) + min;
    
}

