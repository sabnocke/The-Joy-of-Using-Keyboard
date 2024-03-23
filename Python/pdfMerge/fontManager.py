import pprint
import typing
import yaml
import os


def load_yaml(yaml_file: str):
    if not os.path.exists(yaml_file):
        raise FileNotFoundError("No such file")
    with open(yaml_file, 'r') as f:
        loaded = yaml.load(f, Loader=yaml.FullLoader)
    return loaded


def write_yaml(source: typing.Dict, destination: os.PathLike | str):
    if not os.path.exists(destination):
        raise FileNotFoundError("No such file")
    with open(destination, 'w') as f:
        yaml.dump(source, f, default_flow_style=False, indent=4)


def main():
    loaded = load_yaml("config.yml")
    list_fonts = set(loaded["listFonts"])
    font_path = {}
    for font in os.scandir(r".\fonts"):
        if font not in list_fonts:
            list_fonts.add(font.name)
            font_path[font.name] = font.path

    pprint.pprint(font_path)
    loaded["fontPath"] = font_path
    loaded["fontCount"] = len(list_fonts)
    write_yaml(loaded, "config.yml")


if __name__ == "__main__":
    main()
