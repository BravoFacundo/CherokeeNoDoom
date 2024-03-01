*This readme is written in SPANISH. If you prefer, you can read this in [English](README.md).*

# Cherokee No Doom

[En progreso] Videojuego de disparos basado en gráficos “Billboard” o 2.5 Dimensiones. Hecho en Unity, C#.

## Tecnologías

Desarrollado utilizando:
- Lenguaje C#
- Unity 2022.3.7f1

*Puedes ver el código [aquí](Assets/_Scripts/).*

## Idea y desarrollo

Este videojuego está diseñado para explorar el desarrollo de un shooter retro así como la implementación de gráficos billboard en un juego de disparos y precisión.

Comúnmente el billboarding es utilizado sólo como apariencia estética y no se relaciona con el entorno 3D, de este modo no dificulta otros sistemas (como las colisiones).
El desafío propuesto aquí es utilizar el sprite como sistema de detección de colisiones pixel perfect o de alta precisión visual.

Realizar un shooter retro me permite limitar el tamaño del proyecto y diferentes aspectos que complejizan la utilización de gráficos billboard, tales como: animaciones necesarias, ángulos de visualización, reacciones a mecánicas, etc.

## Implementación

Para lograr la utilización del sprite como hitbox se implementó un sistema de raycast con detección de coordenadas de impacto sobre la textura del sprite.
Posteriormente se chequea si el pixel en esa coordenada pertenece al alpha de la imagen.

Si el impacto del Raycast sobre la textura pertenece a la parte visible de la imagen entonces se considera impacto.
Si pertenece a la parte invisible de la imagen (su alpha) entonces no se considera impacto y es descartado.

De igual manera se puede chequear la misma coordenada del impacto sobre distintas texturas almacenadas (Que también pueden estar animadas).
Permitiendo determinar áreas que multiplican el daño o activan (Trigger) determinados eventos.

*Mira el script de ejemplo [aquí](Assets/_Scripts/_Examples/SpriteImpactDetectionExample.cs).*

## Galería de imágenes

![Github_CherokeeNoDoom_01](https://github.com/BravoFacundo/CherokeeNoDoom/assets/88951560/4e8304ef-d85f-4e76-b2e9-9cdf6a6aea3a)
![Github_CherokeeNoDoom_02](https://github.com/BravoFacundo/CherokeeNoDoom/assets/88951560/3f080416-4e23-4f9e-acc7-25a29de84679)
![Github_CherokeeNoDoom_03](https://github.com/BravoFacundo/CherokeeNoDoom/assets/88951560/fe61b91e-d11c-45bd-90f3-8ed5cfb540c9)

## Créditos y links

Se reutilizaron las animaciones de un proyecto colaborativo previo (videojuego).

Artistas 2D:
- Dose (Jose Bayugar): [Behance](https://www.behance.net/bayugarj79c4) - [Instagram](https://www.instagram.com/dose_jb/)
- Ikumi: [Instagram](https://www.instagram.com/ikumi_arte/)

Modelos 3D:
- Geronimo Calderon: [Artstation](https://scarymons7ers.artstation.com/).
