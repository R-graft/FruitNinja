using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using winterStage;

public class EntryPoint : MonoBehaviour
{
    [SerializeField] private ScreenSizeHandler _screenSizeHandler;

    [SerializeField] private SpawnZoneController _spawnZoneController;

    [SerializeField] private BlocksController _blocksController;

    [SerializeField] private SpawnSystem _spawnSystem;

    [SerializeField] private BladeHandler _bladeHandler;

    private void Awake()
    {
        _screenSizeHandler.Init();
        _blocksController.Init();
        _spawnZoneController.Init();
        _spawnSystem.Init();
        _bladeHandler.Init();
    }
}
