import cv2
import time
import numpy as np
from grayscale import Matrix, Point
from typing import Any
from numba import njit

matrix = np.array([[0, 2], [3, 1]])


def dithering(image):
    for x in range(image.shape[0]):
        for y in range(image.shape[1]):
            pixel = image[x][y]
            loc_x = x % matrix.shape[0]
            loc_y = y % matrix.shape[1]
            for i in range(3):
                val = matrix[loc_x][loc_y] * 64
                pixel[i] = 0 if pixel[i] <= val else 255
            image[x][y] = pixel
    return image


def helper_function(point: tuple[Point, Any]):
    loc_x = point[0].x % 2
    loc_y = point[0].y % 2
    _matrix = np.array([[0, 2], [3, 1]])
    val = _matrix[loc_x][loc_y] * 256 / 4
    point[1][0] = 0 if point[1][0] <= val else 255
    point[1][1] = 0 if point[1][1] <= val else 255
    point[1][2] = 0 if point[1][2] <= val else 255
    return point


@njit(parallel=True)
def process_rows(row: np.ndarray, row_index: int):
    n_row = np.zeros(row.shape, dtype=np.uint32)
    for x in range(0, row.shape[0]):
        pixel = row[x]
        loc_x = row_index % 2
        loc_y = x % 2
        _matrix = np.array([[0, 2], [3, 1]])
        val = _matrix[loc_x][loc_y] * 256 / 4
        n_row[x] = np.array([0 if pixel[i] <= val else 255 for i in range(3)])
    return n_row, row_index


def image_iterator(image: Matrix):
    for x in range(image.shape[0]):
        for y in range(image.shape[1]):
            yield Point(x, y), image[x][y]


def process(image: Matrix, pixel: tuple[Point, list]):
    now = time.time()
    proc = helper_function(pixel)
    exc: bool = False
    try:
        point, rgb = pixel
        image[point.x][point.y] = proc[1]
        print(f"Processed {point.x}x{point.y}")
        exc = True
    except Exception as e:
        print(f"Encountered an error <process>: {e}")
    finally:
        return exc, now - time.time()


@njit
def dithering_parallel(image):
    for row_index in range(image.shape[0]):
        n_row_i = process_rows(image[row_index], row_index)
        print(f"Processing row {row_index}")
        image[n_row_i[1]] = n_row_i[0]
    return image


def main() -> None:
    image = cv2.imread('./10.png')
    then: float = time.time()
    img = dithering(image)
    # img = dithering_parallel(image)
    now: float = time.time()
    cv2.imshow('image', img)
    cv2.waitKey(0)
    cv2.destroyAllWindows()
    print(f"Time taken: {now - then:.3f}s")


if __name__ == "__main__":
    main()
