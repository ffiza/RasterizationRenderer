<div align="center">
    <h1>Rasterization Renderer</h1>
</div>

<p align="center">
    <img src="https://forthebadge.com/images/badges/made-with-c-sharp.svg"></a>
</p>

A very simple 3D renderer that uses rasterization. All code is written in C#.

This program calculates the projection of 3D entities onto a given viewport and draws the projected image onto a canvas. Each entity is implemented as a list of triangles, a color, and a transform. The transform allows the application of scaling, rotation, and translation to the model. Each triangle, in turn, is composed of a series of vertices that can be scaled, rotated, and translated in 3D space.

This project is inspired by the book Computer Graphics from Scratch by Gabriel Gambetta.

<p align="center">
    <img src="https://i.imgur.com/UZAvzLy.png" width=400></a>
</p>
<p align="center">An example of a 3D scene rendered at the current state of the project.</p>

Future Work:

- [ ] Implement camera clipping.
- [ ] Implement the removal of hidden surfaces.
- [ ] Add shading for entity colors.
- [ ] Add textures for entities.
- [ ] Add primitives other than cubes.