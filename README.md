# GPU Instance Manager
GPU Instance Manager provides a lot of batch savings to improve your game performance in Unity. GPU Instancing is a good way to improve your performance issues and also battery usages. Without GPU Instancing, Unity draws every single mesh one by one. For example, you have 300 cubes, Unity will create 300 draw call so there will be 300 batch (too bad). With this asset, you can draw 300 cubes using only 1 draw call. (Without shadows etc.)

### When should I use GPU Instancing?

 - If you are using same mesh and same material more than one. (bullets etc.) and
 - If your objects can not be static. (If you can use static, use static and do not use gpu instancing)

### to-DO

 - [x] Add shadow related parameters
 - [ ] Create upm package from asset to easy installing.
 - [ ] Add ability to draw more than 1023 meshes.

### How to use?

 1. Add **GPUInstanceService** prefab to your scene.
 2. Add **GPUInstanceComponent** to your object that it exists MeshRenderer and MeshFilter.
 => On GPUInstanceComponent,  **uniqueMeshId** field provides using ability with same mesh and different materials at the same time. If you want to use your mesh with different material variations, you should give an integer id to separate your components. Otherwise, it is always be 0 (zero) and all meshes will be draw with same material.
 
Your component sends its data to GPUInstanceManager to add, update or remove itself. So, that 's all.

### Important notes

 - If your gameobject has not MeshRenderer or MeshFilter, you should uncheck auto-find checkboxes and assign manually. (Also you can always set your mesh, material and transform target assignments manually.)
 - This asset provides only 1023 meshes at the same time. If you want more than, do not use this asset. (for now.. It will be added with next updates.)
