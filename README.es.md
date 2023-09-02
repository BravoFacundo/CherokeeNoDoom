# Cherokee No Doom

[En progreso] Videojuego de disparos basado en gráficos “Billboard” o 2.5 Dimensiones. Hecho en Unity, C#.

*If you prefer, you can read this in [English](README.md)*

# Tecnologías

Desarrollado utilizando:
- Lenguaje C#
- Unity 2021

*Puedes ver el código [aquí](Assets/Scripts/).*

# Idea y desarrollo

Este videojuego está diseñado para explorar el desarrollo de un shooter retro así como la implementación de gráficos billboard en un juego de disparos y precisión.

Comúnmente el billboarding es utilizado sólo como apariencia estética y no se relaciona con el entorno 3D, de este modo no dificulta otros sistemas (como las colisiones).
El desafío propuesto aquí es utilizar el sprite como sistema de detección de colisiones pixel perfect o de alta precisión visual.

Realizar un shooter retro me permite limitar el tamaño del proyecto y diferentes aspectos que complejizan la utilización de gráficos billboard, tales como: animaciones necesarias, ángulos de visualización, reacciones a mecánicas, etc.

# Implementación

Para lograr la utilización del sprite como hitbox se implementó un sistema de raycast con detección de coordenadas de impacto sobre la textura del sprite.
Posteriormente se chequea si el pixel en esa coordenada pertenece al alpha de la imagen. *Mira el script [aquí](Assets/Scripts/Test.cs).*

Si el impacto del Raycast sobre la textura pertenece a la parte visible de la imagen entonces se considera impacto.
Si pertenece a la parte invisible de la imagen (su alpha) entonces no se considera impacto y es descartado.

De igual manera se puede chequear la misma coordenada del impacto sobre distintas texturas almacenadas (Que también pueden estar animadas).
Permitiendo determinar áreas que multiplican el daño o activan (Trigger) determinados eventos.

# Créditos y links

Se reutilizaron las animaciones de un proyecto colaborativo previo (videojuego).

Artistas:
- Dose (Jose Bayugar): [Behance](https://www.behance.net/bayugarj79c4) - [Instagram](https://www.instagram.com/dose_jb/)
- Ikumi: [Instagram](https://www.instagram.com/ikumi_arte/)
