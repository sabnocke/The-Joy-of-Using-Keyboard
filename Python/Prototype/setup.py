from Cython.Build import cythonize
from setuptools import setup

setup(
    name='testing',
    ext_modules=cythonize('tt.pyx'),
    zip_safe=False
)
