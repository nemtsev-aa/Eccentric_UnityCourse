using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loot : MonoBehaviour
{
    [Tooltip("Коллайдер")]
    [SerializeField] private Collider _collider;
    [Tooltip("Время полёта к сборщику")]
    [SerializeField] private float _timeToCollector;

    public void Collect(Collector collector)
    {
        _collider.enabled = false; // Отключаем коллайдер лута, чтобы обеспечить одноразовый сбор
        StartCoroutine(MoveToCollector(collector)); // Визуализируем сбор
    }

    private IEnumerator MoveToCollector(Collector collector)
    {
        Vector3 a = transform.position; // Текущее положение лута
        Vector3 b = a + Vector3.up * 2f; // Смещение №1 для создания кривой Безье


        for (float t = 0; t < 1f; t+=Time.deltaTime / _timeToCollector)
        {
            Vector3 d = collector.transform.position; // Текущее положение сборщика
            Vector3 c = d + Vector3.up * 2f; // Смещение №2 для создания кривой Безье

            Vector3 position = Bezier.GetPoint(a, b, c, d, t);
            transform.position = position; // Новое положение лута на пути к сборщику
            yield return null;
        }
        Take(collector);
    }

    public virtual void Take(Collector collector)
    {
        Destroy(gameObject); // Унижтожаем лут после сбора
    }
}
