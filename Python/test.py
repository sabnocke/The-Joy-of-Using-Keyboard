from collections import UserList, namedtuple
from typing import Any, Iterable, Literal, Union, Callable, TypeVar
from bs4 import BeautifulSoup
import requests
from pprint import pprint
import validators
import re
import scrapy
from time import time
from urllib.parse import urlparse
import sys

Boolean = Union[bool, Literal[1], Literal[0]]
null = TypeVar("null")

Config = namedtuple("Config", ["unique"])
Connection = namedtuple("Connection", ["url", "extension"])


def test(fn: Callable, *args, expected_error_return=null, compact=True) -> Any | tuple[Any, Any]:
    stdout: Any = None
    stderr: Any = None
    try:
        stdout = fn(*args)
    except Exception as e:
        stderr = e if expected_error_return == null else expected_error_return
    finally:
        return stdout if compact else stdout, stderr


class HashList(UserList):
    def __init__(self, unique: Boolean = 0):
        super().__init__()
        self.config = Config(unique)

    def index(self, item: Any, __start=..., __stop=..., default_value: Any = -1) -> int:
        return test(self.data.index, item, expected_error_return=default_value, compact=True)

    @property
    def unique(self) -> bool:
        return len(self.data) == len(set(self.data))

    @property
    def length(self) -> int:
        return len(self.data)

    def difference(self, comparer: Iterable):
        return [item for item in self.data if item not in comparer]

    def append(self, _t: Any) -> None:
        if not (self.config.unique and _t in self.data):
            self.data.append(_t)

    def reconfigure(self, cfg: Config):
        self.config = cfg


def is_url(_url: str) -> bool:
    return bool(validators.url(_url.strip()))


def is_match(match: str, patter: str) -> bool:
    return bool(re.search(patter, match))


def find_extension(item: str):
    return re.search(r"(epub|mobi|pdf)", item, re.IGNORECASE).group()


# page = requests.get("https://annas-archive.org/md5/2b0f7d7d78617b33d70fc9bac73417f2")
# # print(f"{response_alter}")
# soup = BeautifulSoup(page.content, "html.parser")
# seek = soup.find(id="md5-panel-downloads")
# urlSet = HashList(1)
# if seek:
#     depth_seek_ul = seek.find_all("ul", class_="mb-4")
#     for _item in depth_seek_ul:
#         depth_seek_li = _item.find_all_next(name="li")
#         for i in depth_seek_li:
#             url = i.find_next('a').get('href')
#             # print(url)
#             if is_url(url) and is_match(url, r"ipfs"):
#                 urlSet.append(url)


class BookPlunderer:
    def __init__(self, query: str = "", proto=None):
        self.query = query
        self.page: requests.Response  # Hypothetical response for build purposes only!
        self.proto_page = requests.get("https://annas-archive.org/md5/1997904a67ca36f59da624c83f1b61d7")
        self.url_parse = urlparse("https://annas-archive.org/md5/1997904a67ca36f59da624c83f1b61d7")
        self.soup: BeautifulSoup = BeautifulSoup(self.proto_page.content, "html.parser")
        self.seek_list = HashList(1)
        self.proto_list = HashList(1)
        self.prototype = proto
        self.new_url = HashList(1)

    def create_url(self):
        """
        :raise NotImplementedError: It just doesn't work
        :return: null
        """
        raise NotImplementedError("Still doesn't work!")

    def load_urls(self):
        if self.soup:
            depth_seek = self.soup.find_all("li")
            for item in depth_seek:
                uri = item.find_next('a').get('href')
                self.proto_list.append(uri)
                if is_url(uri) and is_match(uri, "ipfs"):
                    self.seek_list.append(uri)
                elif is_match(uri, "slow_download"):
                    self.new_url.append(self.url_parse.scheme + r"://" + self.url_parse.netloc + uri)

    def download_url(self):
        if self.seek_list.length > 0:
            for _i in self.seek_list:
                page = requests.get(_i)
                if page.status_code == 200:
                    self.__download_url(Connection(page, find_extension(_i)))
                    break
        else:
            print("got there")
            self.__download_url(self.__alternative())

    @staticmethod
    def __download_url(conn: Connection):
        print(conn)
        # sys.exit(-1)
        t0 = time()
        _c = requests.get(conn.url)
        if _c.status_code != 200:
            raise Exception  # TODO change to something better
        try:
            with open(f"book.{conn.extension}", mode="wb") as file:
                file.write(_c.content)
        except Exception as e:
            print("Unhandled Exception: %s" % e)
        finally:
            print(f"Time elapsed: {time() - t0:.2f}")

    def __alternative(self):
        page: requests.Response | None = None
        for _i in self.new_url:
            page = requests.get(_i)
            if page.status_code == 200:
                break
        if page:
            temp_soup = BeautifulSoup(page.content, "html.parser")
            obj = temp_soup.find_all("p", class_="mb-4")
            print("alternative")
            for _i in obj:
                url = _i.find_next('a').get('href')
                if is_url(url):
                    return Connection(url, find_extension(url))

    @property
    def status(self):
        return self.proto_page.status_code, self.proto_page.ok


bp = BookPlunderer()
bp.load_urls()
print(bp.seek_list)
print(bp.new_url)
bp.download_url()

# TODO maybe implement parallelism for multiple files?
# TODO activate request stream requests.get(curl, stream=True)

