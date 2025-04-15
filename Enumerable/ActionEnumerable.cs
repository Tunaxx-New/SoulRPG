using System;
using System.Collections;
using System.Collections.Generic;

public class ActionEnumerable : IEnumerable<string>
{
    // Fields with values
    public static List<string> titles;
    public static List<string> descriptions;
    public static List<int> ids;
    public static readonly int IGNORE = 0;
    public static readonly int PICK_UP = 1;

    // Constructor that initializes the list of titles
    public ActionEnumerable()
    {
        titles = new List<string>
        {
            "Ignore",
            "PickUp"
        };
        descriptions = new List<string>
        {
            "Ignores action to object",
            "Pick up object"
        };
        ids = new List<int>
        {
            IGNORE,
            PICK_UP
        };
    }

    // The GetEnumerator method is required to implement IEnumerable
    public IEnumerator<string> GetEnumerator()
    {
        foreach (var title in titles)
        {
            yield return title;
        }
    }

    // Non-generic GetEnumerator for backward compatibility
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}