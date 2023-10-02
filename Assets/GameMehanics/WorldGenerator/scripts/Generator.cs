using UnityEngine;

namespace GameMehanics.WorldGenerator {
    public class Generator : MonoBehaviour {

        private const int MATERICS_WIDTH = 4, MATERICS_HEIGHT = 4;

        [SerializeField] private Transform _prefab;

        private void Awake() {
            int zoom4096_w = 4, zoom4096_h = 4;
            int[,] materics = new int[zoom4096_h, zoom4096_w];

            for (int y = 0; y < MATERICS_HEIGHT; y++)
                for (int x = 0; x < MATERICS_WIDTH; x++)
                    if (Random.Range(0, 5) == 0)
                        materics[y, x] = 1;

            Debug.Log(materics.Debug());

            int zoom2048_w = zoom4096_w * 2, zoom2048_h = zoom4096_h * 2;

            int[,] zoom2048 = new int[zoom2048_h, zoom2048_w];

            for (int y = 0; y < zoom2048_h; y++)
                for (int x = 0; x < zoom2048_w; x++) {
                    int index = materics[y / 2, x / 2];

                    if (index == 0) {
                        if (Random.Range(0, 10) == 0) {
                            zoom2048[y, x] = 1;

                            continue;
                            }
                        }
                    else {
                        if (Random.Range(0, 5) == 0) {
                            zoom2048[y, x] = 0;

                            continue;
                            }
                        }

                    zoom2048[y, x] = index;
                    }

            Debug.Log(zoom2048.Debug());

            int[,] zoom2048new = new int[zoom2048_h, zoom2048_w];

            for (int y = 0; y < zoom2048_h; y++)
                for (int x = 0; x < zoom2048_w; x++) {
                    if (zoom2048[y, x] == 0) {
                        if (GetNeighborCount(zoom2048, y, x, 1) > 0) {
                            if (Random.Range(0, 2) == 0) {
                                zoom2048new[y, x] = 1;

                                continue;
                                }
                            }
                        }
                    else {
                        if (GetNeighborCount(zoom2048, y, x, 0) > 0) {
                            if (Random.Range(0, 2) == 0) {
                                zoom2048new[y, x] = 0;

                                continue;
                                }
                            }
                        }

                    zoom2048new[y, x] = zoom2048[y, x];
                    }

            Debug.Log(zoom2048new.Debug());

            int zoom1024_w = zoom2048_w * 2, zoom1024_h = zoom2048_h * 2;

            int[,] zoom1024 = new int[zoom1024_h, zoom1024_w];

            for (int y = 0; y < zoom1024_h; y++)
                for (int x = 0; x < zoom1024_w; x++) {
                    int index = zoom2048new[y / 2, x / 2];

                    if (index == 0) {
                        if (Random.Range(0, 10) == 0) {
                            zoom1024[y, x] = 1;

                            continue;
                            }
                        }
                    else {
                        if (Random.Range(0, 5) == 0) {
                            zoom1024[y, x] = 0;

                            continue;
                            }
                        }

                    zoom1024[y, x] = index;
                    }

            int[,] zoom1024new = new int[zoom1024_h, zoom1024_w];

            for (int y = 0; y < zoom1024_h; y++)
                for (int x = 0; x < zoom1024_w; x++) {
                    if (zoom1024[y, x] == 0) {
                        if (GetNeighborCount(zoom1024, y, x, 1) > 0) {
                            if (Random.Range(0, 5) == 0) {
                                zoom1024new[y, x] = 1;

                                continue;
                                }
                            }
                        }
                    else {
                        if (GetNeighborCount(zoom1024, y, x, 0) > 0) {
                            if (Random.Range(0, 2) == 0) {
                                zoom1024new[y, x] = 0;

                                continue;
                                }
                            }
                        }

                    zoom1024new[y, x] = zoom1024[y, x];
                    }

            int[,] zoom1024new2 = new int[zoom1024_h, zoom1024_w];

            for (int y = 0; y < zoom1024_h; y++)
                for (int x = 0; x < zoom1024_w; x++) {
                    if (zoom1024new[y, x] == 0) {
                        if (GetNeighborCount(zoom1024new, y, x, 1) > 0) {
                            if (Random.Range(0, 5) == 0) {
                                zoom1024new2[y, x] = 1;

                                continue;
                                }
                            }
                        }
                    else {
                        if (GetNeighborCount(zoom1024new, y, x, 0) > 0) {
                            if (Random.Range(0, 2) == 0) {
                                zoom1024new2[y, x] = 0;

                                continue;
                                }
                            }
                        }

                    zoom1024new2[y, x] = zoom1024new[y, x];
                    }

            int[,] zoom1024new22 = new int[zoom1024_h, zoom1024_w];

            for (int y = 0; y < zoom1024_h; y++)
                for (int x = 0; x < zoom1024_w; x++) {
                    if (zoom1024new2[y, x] == 0) {
                        if (GetNeighborCount(zoom1024new2, y, x, 1) > 0) {
                            if (Random.Range(0, 5) == 0) {
                                zoom1024new22[y, x] = 1;

                                continue;
                                }
                            }
                        }
                    else {
                        if (GetNeighborCount(zoom1024new2, y, x, 0) > 0) {
                            if (Random.Range(0, 2) == 0) {
                                zoom1024new22[y, x] = 0;

                                continue;
                                }
                            }
                        }

                    zoom1024new22[y, x] = zoom1024new2[y, x];
                    }

            int[,] removeOcean = new int[zoom1024_h, zoom1024_w];

            for (int y = 0; y < zoom1024_h; y++)
                for (int x = 0; x < zoom1024_w; x++) {
                    if (zoom1024new22[y, x] == 0) {
                        if (GetNeighborCount(zoom1024new2, y, x, 0) > 7) {
                            if (Random.Range(0, 2) == 0) {
                                removeOcean[y, x] = 1;

                                continue;
                                }
                            }
                        }
                    removeOcean[y, x] = zoom1024new22[y, x];
                    }

            int zoom512_w = zoom1024_w * 2, zoom512_h = zoom1024_h * 2;

            int[,] zoom512 = new int[zoom512_h, zoom512_w];

            for (int y = 0; y < zoom512_h; y++)
                for (int x = 0; x < zoom512_w; x++) {
                    int index = removeOcean[y / 2, x / 2];

                    if (index == 0) {
                        if (Random.Range(0, 10) == 0) {
                            zoom512[y, x] = 1;

                            continue;
                            }
                        }
                    else {
                        if (Random.Range(0, 5) == 0) {
                            zoom512[y, x] = 0;

                            continue;
                            }
                        }

                    zoom512[y, x] = index;
                    }

            int zoom256_w = zoom512_w * 2, zoom256_h = zoom512_h * 2;

            int[,] zoom256 = new int[zoom256_h, zoom256_w];

            for (int y = 0; y < zoom256_h; y++)
                for (int x = 0; x < zoom256_w; x++) {
                    int index = zoom512[y / 2, x / 2];

                    if (index == 0) {
                        if (Random.Range(0, 10) == 0) {
                            zoom256[y, x] = 1;

                            continue;
                            }
                        }
                    else {
                        if (Random.Range(0, 5) == 0) {
                            zoom256[y, x] = 0;

                            continue;
                            }
                        }

                    zoom256[y, x] = index;
                    }

            int zoom128_w = zoom256_w * 2, zoom128_h = zoom256_h * 2;

            int[,] zoom128 = new int[zoom128_h, zoom128_w];

            for (int y = 0; y < zoom128_h; y++)
                for (int x = 0; x < zoom128_w; x++) {
                    int index = zoom256[y / 2, x / 2];

                    if (index == 0) {
                        if (Random.Range(0, 10) == 0) {
                            zoom128[y, x] = 1;

                            continue;
                            }
                        }
                    else {
                        if (Random.Range(0, 5) == 0) {
                            zoom128[y, x] = 0;

                            continue;
                            }
                        }

                    zoom128[y, x] = index;
                    }

            int zoom64_w = zoom128_w * 2, zoom64_h = zoom128_h * 2;

            int[,] zoom64 = new int[zoom64_h, zoom64_w];

            for (int y = 0; y < zoom64_h; y++)
                for (int x = 0; x < zoom64_w; x++) {
                    int index = zoom128[y / 2, x / 2];

                    if (index == 0) {
                        if (Random.Range(0, 10) == 0) {
                            zoom64[y, x] = 1;

                            continue;
                            }
                        }
                    else {
                        if (Random.Range(0, 5) == 0) {
                            zoom64[y, x] = 0;

                            continue;
                            }
                        }

                    zoom64[y, x] = index;
                    }

            int zoom32_w = zoom64_w * 2, zoom32_h = zoom64_h * 2;

            int[,] zoom32 = new int[zoom32_h, zoom32_w];

            for (int y = 0; y < zoom32_h; y++)
                for (int x = 0; x < zoom32_w; x++) {
                    int index = zoom64[y / 2, x / 2];

                    if (index == 0) {
                        if (Random.Range(0, 10) == 0) {
                            zoom32[y, x] = 1;

                            continue;
                            }
                        }
                    else {
                        if (Random.Range(0, 5) == 0) {
                            zoom32[y, x] = 0;

                            continue;
                            }
                        }

                    zoom32[y, x] = index;
                    }

            int zoom16_w = zoom32_w * 2, zoom16_h = zoom32_h * 2;

            int[,] zoom16 = new int[zoom16_h, zoom16_w];

            for (int y = 0; y < zoom16_h; y++)
                for (int x = 0; x < zoom16_w; x++) {
                    int index = zoom32[y / 2, x / 2];

                    if (index == 0) {
                        if (Random.Range(0, 10) == 0) {
                            zoom16[y, x] = 1;

                            continue;
                            }
                        }
                    else {
                        if (Random.Range(0, 5) == 0) {
                            zoom16[y, x] = 0;

                            continue;
                            }
                        }

                    zoom16[y, x] = index;
                    }

            for (int y = 0; y < zoom2048_w; y++)
                for (int x = 0; x < zoom2048_h; x++) {
                    if (zoom2048new[y, x] == 1)
                        Instantiate(_prefab, new Vector3(x, -y, 0), Quaternion.identity);
                    }
            }

        private int GetNeighborCount(int[,] map, int y, int x, int neighbor) {
            int[,] mask = { { -1, 0 }, { 1, 0 }, { 0, -1 }, { 0, 1 }, { -1, -1 }, { 1, -1 }, { -1, 1 }, { 1, 1 } };

            int counter = 0;
            int height = map.GetUpperBound(0) + 1;
            int width = map.Length / height;

            for (int i = 0; i < mask.GetUpperBound(0) + 1; i++) {
                int yy = y + mask[i, 0];
                int xx = x + mask[i, 1];

                if (xx < 0 || xx >= width || yy < 0 || yy >= height)
                    continue;

                if (map[yy, xx] == neighbor)
                    counter++;
                }

            return counter;
            }

        }
    }