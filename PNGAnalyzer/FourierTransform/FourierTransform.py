import argparse
import cv2 as cv
import numpy as np
from matplotlib import pyplot as plt

parser = argparse.ArgumentParser()
parser.add_argument("path", help="path to an image")
args = parser.parse_args()

image = cv.imread(args.path, cv.IMREAD_GRAYSCALE)
transform = cv.dft(np.float32(image), flags=cv.DFT_COMPLEX_OUTPUT)
shifted_transform = np.fft.fftshift(transform)
magnitude, phase = cv.cartToPolar(shifted_transform[:, :, 0], shifted_transform[:, :, 1])
magnitude = 20 * np.log(magnitude)

plt.figure("Fourier Transform")
plt.subplot(2, 1, 1), plt.imshow(image, cmap='gray')
plt.title('Image'), plt.xticks([]), plt.yticks([])
plt.subplot(2, 2, 3), plt.imshow(magnitude, cmap='gray')
plt.title('Magnitude'), plt.xticks([]), plt.yticks([])
plt.subplot(2, 2, 4), plt.imshow(phase, cmap='gray')
plt.title('Phase'), plt.xticks([]), plt.yticks([])
plt.show()
