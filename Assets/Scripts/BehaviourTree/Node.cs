using System.Collections;
using System.Collections.Generic;


public enum NodeState 
{
	SUCCESS,
	RUNNING,
	FAILED
}
public class Node 
{
	public Node parent;

	protected List<Node> children = new List<Node>();
	protected NodeState state;

	private Dictionary<string, object> dataContext =
		new Dictionary<string, object>();

	public Node() 
	{
		parent = null;
	}
	public Node(List<Node> children) 
	{
		for(int i = 0; i < children.Count; i++) 
		{
			Attach(children[i]);
		}
	}
	public void SetData(string key, object value)
	{
		dataContext[key] = value;
	}
	public object GetData(string key)
	{
		object val = null;
		if(dataContext.TryGetValue(key, out val))
			return val;

		Node node = parent;
		if(node != null)
			val = node.GetData(key);
		return val;
	}
	public bool ClearData(string key)
	{
		bool cleared = false;
		if(dataContext.ContainsKey(key))
		{
			dataContext.Remove(key);
			return true;
		}

		Node node = parent;
		if(node != null)
			cleared = node.ClearData(key);
		return cleared;
	}
	public virtual NodeState Evaluate() => NodeState.FAILED;

	private void Attach(Node node)
	{
		node.parent = this;
		children.Add(node);
	}
}
