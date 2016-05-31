/**
 * Original code written by @seb_ly, as presented on Trondheim Developer Conference in 2015
 * No animals were harmed during the production of this code
 *
 * @digitaldias
 *
 * refactored code to avoid using "for", "this" and "new" as they are considered harmful in JavaScript.
 * This code passes JSLint.com if you assume browser.
 */

var treeHeight = 120;

((function (treeHeight) {
    'use strict';
    function random(max, min) {
        return Math.random() * (max - min) + min;
    }

    function setupCanvas() {
        var newCanvas = document.createElement("canvas");
        newCanvas.width = window.innerWidth;
        newCanvas.height = window.innerHeight;
        document.body.appendChild(newCanvas);
        document.body.style.backgroundColor = 'black';
        return newCanvas;
    }
    var canvas = setupCanvas();
    var ctx = canvas.getContext("2d");


    function branch(size, rotation, first) {
        var children = [];
        var sway = 0;
        var swaySpeed = random(0.02, 0.2);

        if (size > 15) {
            children.push(branch(size * random(0.7, 0.9), random(15, 30), false));
            children.push(branch(size * random(0.7, 0.9), random(-15, -30), false));
        }

        var incrementSway = function () {
            sway += swaySpeed;
        };

        var getSway = function () {
            return sway;
        };

        var render = function (context) {
            context.save();
            context.rotate((rotation + Math.sin(getSway())) * Math.PI / 180);
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
            incrementSway();
            context.restore();
        };


        return {
            getSway: getSway,
            children: children,
            first: first,
            incrementSway: incrementSway,
            render: render
        };

    }
    var tree = branch(treeHeight, 0, true);


    function animate() {
        ctx.save();
        ctx.clearRect(0, 0, canvas.width, canvas.height);
        ctx.translate(canvas.width / 2, canvas.height - 100);
        tree.render(ctx);
        window.requestAnimationFrame(animate);
        ctx.restore();
    }
    return {tree: tree, draw: animate};
}(treeHeight)).draw());