using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandInvoker : MonoBehaviour
{
	[SerializeField]
	public static Queue<ICommand> commandBuffer;
	[SerializeField]
	public static List<ICommand> commandHistory;
	[SerializeField]
	public static int counter = 0;

	private void Awake()
	{
		commandBuffer = new Queue<ICommand>();
		commandHistory = new List<ICommand>();
	}

	public static void AddCommand(ICommand command)
	{
		// we're somewhere not at the end of the history
		while(commandHistory.Count > counter)
		{
			commandHistory.RemoveAt(counter);
		}
		
		commandBuffer.Enqueue(command);
	}

    // Update is called once per frame
    private void Update()
    {
        if(commandBuffer.Count > 0)
		{
			ICommand c = commandBuffer.Dequeue();
			c.Execute();

			commandHistory.Add(c);
			counter++;
			Debug.Log("Command history length " + commandHistory.Count);
		}
		else
		{
			// should probably be put in a Input Manager script, but will do the tricks for now
			if(Input.GetKeyDown(KeyCode.Z))
			{
				if(counter > 0)
				{
					counter--;
					commandHistory[counter].Undo();
				}
			}
			else if(Input.GetKeyDown(KeyCode.R))
			{
				if(counter < commandHistory.Count)
				{
					commandHistory[counter].Execute();
					counter++;
				}
			}
		}

		if(Input.GetKeyDown(KeyCode.E))
		{
			ExportLog();
		}
    }

	private static void ExportLog()
	{
		List<string> lines = new List<string>();
		foreach (ICommand c in commandHistory)
		{
			lines.Add(c.ToString());
		}

		int nbLines = lines.Count;
		string[] arrayConvertedLines = new string[nbLines+1];

		for(int i = 0; i < nbLines; ++i)
		{
			arrayConvertedLines[i] = lines[i];
		}

		System.IO.File.WriteAllLines(Application.dataPath + "/commandLog.txt", arrayConvertedLines);
	}
}
