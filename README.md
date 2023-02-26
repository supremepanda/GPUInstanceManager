# GPU Instance Manager

[![Unity 2019.1+](https://img.shields.io/badge/unity-2019.1%2B-blue.svg)](https://unity3d.com/get-unity/download)
[![License: MIT](https://img.shields.io/badge/License-MIT-brightgreen.svg)](https://github.com/dbrizov/NaughtyAttributes/blob/master/LICENSE)

GPU Instance Manager provides a lot of batch savings to improve your game performance in Unity. GPU Instancing is a good way to improve your performance issues and also battery usages. Without GPU Instancing, Unity draws every single mesh one by one. For example, you have 300 cubes, Unity will create 300 draw call so there will be 300 batch (too bad). With this asset, you can draw 300 cubes using only 1 draw call. (Without shadows etc.)

### Installation

1. You can add git url via **Package Manager => Add package from git url**
```
https://github.com/supremepanda/GPUInstanceManager.git#upm
```

2. You can also install via git url by adding this entry in your **manifest.json**
```
"com.supremepanda.gpu_instance_manager": "https://github.com/supremepanda/GPUInstanceManager.git#upm"
```

### When should I use GPU Instancing?

 - If you are using same mesh and same material more than one. (bullets etc.) and
 - If your objects can not be static. (If you can use static, use static and do not use gpu instancing)

### to-DO

 - [x] Add shadow related parameters
 - [x] Create upm package from asset to easy installing.
 - [ ] Add ability to draw more than 1023 meshes.

### How to use?

 1. Add **GPUInstanceService** prefab to your scene.
 2. Add **GPUInstanceComponent** to your object that it exists MeshRenderer and MeshFilter.
 => On GPUInstanceComponent,  **uniqueMeshId** field provides using ability with same mesh and different materials at the same time. If you want to use your mesh with different material variations, you should give an integer id to separate your components. Otherwise, it is always be 0 (zero) and all meshes will be draw with same material.
 
Your component sends its data to GPUInstanceManager to add, update or remove itself. So, that 's all.

### Important notes

 - If your gameobject has not MeshRenderer or MeshFilter, you should uncheck auto-find checkboxes and assign manually. (Also you can always set your mesh, material and transform target assignments manually.)
 - This asset provides only 1023 meshes at the same time. If you want more than, do not use this asset. (for now.. It will be added with next updates.)
