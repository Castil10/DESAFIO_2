# Manual de usuario

## Acceso y requisitos
- Ejecute la aplicación de escritorio **Techopolis Transport** en Windows con .NET 6 o superior.
- No requiere base de datos ni archivos externos; los mapas vienen preconfigurados en el código.

## Elementos principales de la interfaz
- **Mapa pre-cargado**: seleccione el escenario de la ciudad que desea visualizar.
- **Origen / Destino**: combo de estaciones disponibles en el mapa actual.
- **Botones de recorridos**:
  - `Recorrido BFS`: lista las estaciones visitadas en anchura desde el origen.
  - `Recorrido DFS`: lista las estaciones visitadas en profundidad desde el origen.
  - `Ruta más corta (Dijkstra)`: muestra el camino óptimo entre origen y destino con su tiempo total.
- **Rutas y tiempos (min)**: grilla editable; ajuste el peso de cada ruta (tráfico) en minutos.
- **Resultado**: panel de texto con el detalle del algoritmo solicitado.
- **Pizarra**: lienzo interactivo con nodos arrastrables, resaltado de recorridos y pesos en cada arco.

## Flujo de uso
1. Elija un **Mapa pre-cargado**; el grafo se refresca al instante.
2. Seleccione una **estación de origen** (y destino cuando aplique).
3. Opcional: modifique los **tiempos de ruta** en la grilla. Solo se aceptan enteros mayores a 0; el mapa se reordena automáticamente.
4. Ejecute uno de los botones de análisis (BFS, DFS o Ruta más corta). El recorrido se lista y se destaca visualmente en la pizarra.
5. Arrastre los nodos en la pizarra para reorganizar la vista sin perder la información.

## Manejo y almacenamiento
- Los mapas están definidos en código (`MapDefinition`) y pueden extenderse agregando estaciones o rutas nuevas.
- Los cambios de tiempo de ruta se mantienen mientras la aplicación está abierta; al cambiar de mapa se recargan los valores predefinidos.

## Validaciones incluidas
- Selección obligatoria de origen/destino y validación de que sean diferentes para rutas cortas.
- Las celdas de tiempo solo aceptan números enteros positivos; valores inválidos se rechazan con un mensaje.
- El grafo es no dirigido: cada cambio en un peso se aplica en ambos sentidos de la ruta.
