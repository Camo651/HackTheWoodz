using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FlowControl : MonoBehaviour
{
	public Tilemap tileMap;
	public CameraControl cameraControl;
	public Tile previewTile;
	public List<List<Element.ElementType>> previewElements;
	public Tile hoveredTile;
	public int playerScore;
	public int remainingTiles;
	public TextMeshProUGUI scoreText, tilesText;
	public AudioManager am;
	public GameObject scoreParticlePrefab;
	public GameObject scoreParticleHolder;
	public GameObject pauseMenu;
	public bool gameIsPaused;

	private void Start()
	{
		tileMap.CreateTile(Tile.TileType.Ghost, Vector3.zero, false, null);
		SetPreviewTile(GenerateElements(Random.Range(0,100000)));
		gameIsPaused = false;
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			TogglePause();
		}
		if (gameIsPaused)
			return;


		Tile t = cameraControl.RaycastTile();

		if (t)
		{
			if (Input.GetMouseButtonDown(0))
			{
				if (t.tileType == Tile.TileType.Ghost && TileCanBePlaced(t))
				{
					Vector3 pos = t.tilePosition;
					tileMap.DelteTile(t);
					tileMap.CreateTile(Tile.TileType.Basic, pos, true, previewElements);
					SetPreviewTile(GenerateElements(Random.Range(0, 100000)));
				}
			}
			if(t.tileType == Tile.TileType.Ghost)
			{
				if (hoveredTile != t)
				{
					if (hoveredTile)
						hoveredTile.transform.GetComponentInChildren<MeshRenderer>().material = tileMap.ghostNone;
					hoveredTile = t;
					if (TileCanBePlaced(t))
					{
						hoveredTile.transform.GetComponentInChildren<MeshRenderer>().material = tileMap.ghostAllow;
					}
					else
					{
						hoveredTile.transform.GetComponentInChildren<MeshRenderer>().material = tileMap.ghostDeny;
					}
				}
			}
			else if(hoveredTile)
			{
				hoveredTile.transform.GetComponentInChildren<MeshRenderer>().material = tileMap.ghostNone;
				hoveredTile = null;
			}
		}
		else if (hoveredTile)
		{
			hoveredTile.transform.GetComponentInChildren<MeshRenderer>().material = tileMap.ghostNone;
			hoveredTile = null;
		}
		if (Input.GetKeyDown(KeyCode.R))
		{
			RotatePreviewTile();
			if(hoveredTile)
				hoveredTile.transform.GetComponentInChildren<MeshRenderer>().material = tileMap.ghostNone;
			hoveredTile = null;
		}



		previewTile.transform.localEulerAngles = Vector3.up * (360f-cameraControl.transform.parent.localEulerAngles.y);
	}

	public void TogglePause()
	{
		gameIsPaused = !gameIsPaused;
		pauseMenu.SetActive(gameIsPaused);
		Time.timeScale = gameIsPaused ? 0 : 1;

	}
	public void QuitToMenu()
	{
		UnityEngine.SceneManagement.SceneManager.LoadScene(0);
	}

	public bool TileCanBePlaced(Tile ghost)
	{
		if (ghost.tilePosition.y == 0)
			return true;

		bool allElementsMatch = true, containsElements = false;
		List<List<Element>> baseElements = tileMap.GetTileInMap(ghost.tilePosition - (Vector3.up * tileMap.tileDimensions.y)).childedElements;
		for (int x = 0; x < tileMap.tileDimensions.x / tileMap.elementDimensions.x; x++)
		{
			for (int z = 0; z < tileMap.tileDimensions.z / tileMap.elementDimensions.z; z++)
			{
				if (previewElements[x][z] != Element.ElementType.None)
				{
					containsElements = true;
					if(baseElements[x][z] == null || !baseElements[x][z].isStackable)
					{
						allElementsMatch = false;
					}
				}
			}
		}
		return allElementsMatch && containsElements;
	}

	public void SetPreviewTile(List<List<Element.ElementType>> e)
	{
		previewTile.ClearElementsOnTile();
		previewTile.GenerateElementsOnTile(e, false);
		previewElements = e;
	}

	public Element.ElementType[] typeWeights =
	{
		Element.ElementType.Studio,
		Element.ElementType.Hut,
		Element.ElementType.Field,
		Element.ElementType.Field,
		Element.ElementType.CastleWall,
		Element.ElementType.Studio,
	};
	public List<List<Element.ElementType>> GenerateElements(int seed)
	{
		bool hasElements = false;
		List<List<Element.ElementType>> elms = new List<List<Element.ElementType>>();
		int tries = 0;
		while (!hasElements && tries < 100)
		{
			tries++;
			elms = new List<List<Element.ElementType>>();
			for (int x = 0; x < tileMap.tileDimensions.x / tileMap.elementDimensions.x; x++)
			{
				elms.Add(new List<Element.ElementType>());
				for (int z = 0; z < tileMap.tileDimensions.z / tileMap.elementDimensions.z; z++)
				{
					elms[x].Add(Element.ElementType.None);
					if (Mathf.PerlinNoise((seed * 5.01f) + x, (seed * 5.01f) + z) * 10f > 6f)
					{
						hasElements = true;
						elms[x][z] = typeWeights[Random.Range(0, typeWeights.Length)];
					}
				}
			}
			seed = Random.Range(0, 100000);
		}
		return elms;
	}

	public void RotatePreviewTile()
	{
		List<List<Element.ElementType>> remap = new List<List<Element.ElementType>>();
		remap.Add(new List<Element.ElementType>() { previewElements[2][0], previewElements[1][0], previewElements[0][0] });
		remap.Add(new List<Element.ElementType>() { previewElements[2][1], previewElements[1][1], previewElements[0][1] });
		remap.Add(new List<Element.ElementType>() { previewElements[2][2], previewElements[1][2], previewElements[0][2] });
		previewElements = remap;
		SetPreviewTile(previewElements);
	}

	public IEnumerator ThrowScoreParticle(int points)
	{
		Vector3 startPos = new Vector3(0,-100);
		Vector3 endPos = new Vector3(0,80);
		TextMeshProUGUI part = Instantiate(scoreParticlePrefab, scoreParticleHolder.transform).GetComponent<TextMeshProUGUI>();
		part.text = "+" + points;
		for (int i = 0; i < 700; i++)
		{
			yield return null;
			part.rectTransform.anchoredPosition = Vector2.Lerp(startPos, endPos, i / 700f);
			part.color = Color.Lerp(Color.white, Color.clear, i / 700f);
		}
		Destroy(part.gameObject);
	}
}
