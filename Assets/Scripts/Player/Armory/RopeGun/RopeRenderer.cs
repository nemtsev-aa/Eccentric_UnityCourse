using UnityEngine;

public class RopeRenderer : MonoBehaviour
{
    [Tooltip("Компанент - средство визуализации линий")]
    public LineRenderer LineRenderer;
    [Tooltip("Количество сигментов линии")]
    [SerializeField] private int _segmentsCount = 10;

    public void Draw(Vector3 startPoint, Vector3 endPoint, float length)
    {
        LineRenderer.enabled = true;
        // Расстояние между точками
        float interpolant = Vector3.Distance(startPoint, endPoint);
        // Величина провисания линии
        float offset = Mathf.Lerp(length / 2f, 0f, interpolant);
        // Крайняя точка провисания под точкой начала линии
        Vector3 startPointDown = startPoint + Vector3.down * offset;
        // Крайняя точка провисания под точкой конца линии
        Vector3 endPointDown = endPoint + Vector3.down * offset;

        LineRenderer.positionCount = _segmentsCount + 1;
        for (int i = 0; i < _segmentsCount +1; i++)
        {
            // Устанавливаем положение точек линии в соответствии с формой кривой Безье
            LineRenderer.SetPosition(i, Bezier.GetPoint(startPoint, startPointDown, endPointDown, endPoint, (float)i / _segmentsCount));
        }
    }

    public void Hide()
    {
        LineRenderer.enabled = false;
    }

}
