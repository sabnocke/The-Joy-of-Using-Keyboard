from setuptools import setup
from Cython.Build import cythonize

setup(
    name='testing',
    ext_modules=cythonize('tt.pyx'),
    zip_safe=False
)
