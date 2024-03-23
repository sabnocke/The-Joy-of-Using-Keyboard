from pprint import pprint
from typing import Dict, Optional, TypeVar, Any
import yaml
import os

TPathLike = TypeVar('TPathLike', str, bytes, os.PathLike[str], os.PathLike[bytes])


def load_fonts():
    return os.scandir(r".\fonts")


class YamlManager:
    def __init__(self, yaml_path: TPathLike) -> None:
        self.yaml_path = yaml_path
        self.__isValid = os.path.exists(yaml_path)
        self.__yaml = self.__load_yaml()

    @property
    def valid(self) -> bool:
        return self.__isValid

    @valid.setter
    def valid(self,
              value) -> None:
        pass

    @property
    def yml(self):
        return self.__yaml

    def __load_yaml(self) -> Optional[Dict]:
        if not self.valid:
            raise FileNotFoundError("No such file")
        with open(self.yaml_path, 'r') as f:
            return yaml.load(f, Loader=yaml.FullLoader)

    def __write_yaml(self,
                     source: Dict) -> None:
        if not self.valid:
            raise FileNotFoundError("No such file")
        with open(self.yaml_path, 'w') as f:
            yaml.dump(source, f, indent=4)

    def __getitem__(self, item=None) -> Any:
        if isinstance(self.__yaml, dict):
            return self.__yaml[item]
        elif self.__yaml is None:
            return self.__yaml
        else:
            return 0


class Option[T]:
    def __init__(self, value: T) -> None:
        self.value = value


def main():
    ym = YamlManager("config.yml")
    loaded = ym.load_yaml
    list_fonts = set(loaded["fonts"])
    font_path = {}
    for font in os.scandir(r".\fonts"):
        if font in list_fonts:
            continue
        list_fonts.add(font.name[:-4])
        font_path[font.name[:-4]] = font.path

    pprint(font_path)
    pprint(list_fonts)
    loaded["fontPath"] = font_path
    loaded["fontCount"] = len(list_fonts)
    loaded["fonts"] = list_fonts
    ym.write_yaml(loaded)


if __name__ == "__main__":
    main()
