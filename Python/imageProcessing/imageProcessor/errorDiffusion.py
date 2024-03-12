import cv2
import numpy as np
from numpy import ndarray


def process(image):
    height, width, *rest = image.shape
    for x in range(height):
        for y in range(width):
            pixel = image[x][y]
            clr = find_color(pixel)
            err = np.subtract(pixel, clr)
            if (x + 1) < height:
                image[x + 1][y] = np.add(image[x + 1][y], np.multiply(err, 7 / 16))
            if (y + 1) < width and (x - 1) > 0:
                image[x - 1][y + 1] = np.add(image[x - 1][y + 1], np.multiply(err, 3 / 16))
            if (y + 1) < width:
                image[x][y + 1] = np.add(image[x][y + 1], np.multiply(err, 5 / 16))
            if (y + 1) < width and (x + 1) < height:
                image[x + 1][y + 1] = np.add(image[x + 1][y + 1], np.multiply(err, 1 / 16))
    return image


def find_color(pixel: ndarray):
    return np.array([round(value / 255) for value in pixel])


def main() -> None:
    image = cv2.imread('../image (3).png')
    print(image.shape)
    return
    img = process(image)
    cv2.imshow('image', img)
    cv2.waitKey(0)
    cv2.destroyAllWindows()


if __name__ == "__main__":
    main()
