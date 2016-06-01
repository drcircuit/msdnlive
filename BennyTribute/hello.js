/**
 * Original code written by @seb_ly, as presented on Trondheim Developer Conference in 2015
 * No animals were harmed during the production of this code
 *
 * @digitaldias
 *
 * refactored code to avoid using "for", "this" and "new" as they are considered harmful in JavaScript.
 * This code passes JSLint.com if you assume browser.
 * 'use strict'; pragmas added to pass JSLint, if you wrap your entire program in an immediately invoked anonymous
 * function, this only needs to be added once as the first line of the anonymous function.
 */
var window = document.defaultView || document.parentWindow;
function random(max, min) {
    'use strict';
    return Math.random() * (max - min) + min;
}

function setupCanvas() {
    'use strict';
    var newCanvas = document.createElement("canvas");
    newCanvas.width = window.innerWidth;
    newCanvas.height = window.innerHeight;
    document.body.appendChild(newCanvas);
    document.body.style.backgroundColor = 'black';
    return newCanvas;
}

function branch(size, rotation) {
    'use strict';
    var children = [];
    var sway = 0;
    var swaySpeed = random(0.02, 0.2);

    if (size > 15) {
        children.push(branch(size * random(0.7, 0.9), random(15, 30)));
        children.push(branch(size * random(0.7, 0.9), random(-15, -30)));
    }

    return {
        children: children,
        render: function (context) {
            context.save();
            context.rotate((rotation + Math.sin(sway)) * Math.PI / 180);
            context.beginPath();
            context.lineWidth = size * 0.1;
            context.strokeStyle = 'White';
            context.moveTo(0, 0);
            context.lineTo(0, -size);
            context.stroke();
            context.translate(0, -size);
            children.forEach(function (child) {
                child.render(context);
            });
            sway += swaySpeed;
            context.restore();
        }
    };
}
var canvas = setupCanvas();
var ctx = canvas.getContext("2d");
var tree = branch(120, 0);
function animate() {
    'use strict';
    ctx.save();
    ctx.clearRect(0, 0, canvas.width, canvas.height);
    ctx.translate(canvas.width / 2, canvas.height - 100);
    tree.render(ctx);
    window.requestAnimationFrame(animate);
    ctx.restore();
}
animate();