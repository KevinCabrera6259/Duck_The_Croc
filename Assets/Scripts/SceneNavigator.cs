using System.Collections.Generic;

public static class SceneNavigation
{
    private static Stack<string> sceneHistory = new Stack<string>();

    // Guarda la escena actual antes de cambiar
    public static void PushScene(string sceneName)
    {
        sceneHistory.Push(sceneName);
    }

    // Regresa a la escena previa real
    public static string PopScene()
    {
        if (sceneHistory.Count > 0)
            return sceneHistory.Pop();

        return null;
    }

    // Limpia historial (cuando vuelves al menú principal)
    public static void Clear()
    {
        sceneHistory.Clear();
    }
}
