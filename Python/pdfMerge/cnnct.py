import asyncio
import sys

import aiohttp
from aiohttp import ClientSession
from string import Template
import aiofiles
import json
import os
from pprint import pprint
import requests
from bs4 import BeautifulSoup
from functools import wraps
from typing import Any, Callable, Coroutine, TypeVar, TYPE_CHECKING, Union
from Manager import YamlManager, TPathLike
import zipfile

search = "LMRoman12-Bold"

_T = TypeVar("_T")


class IncorrectFileTypeException(Exception):
    pass


def load_uri(api: str, cx: str, num: int = 3, start: int = 0):
    tmplt = Template(
        "https://www.googleapis.com/customsearch/v1?key=$API&cx=$CX&num=$NUM&start=$START&q="
    )
    uri = tmplt.substitute(
        API=api,
        CX=cx,
        NUM=num,
        START=start
    )

    def _(query: str) -> str:
        return uri + query

    return _


def run(fn: Callable[[Any], Coroutine]) -> Callable[[...], _T]:
    @wraps(fn)
    def wrapper(*args,
                **kwargs) -> _T:
        return asyncio.run(wrapped(*args,
                                   **kwargs))

    @wraps(fn)
    async def wrapped(*args,
                      **kwargs) -> Coroutine[Any, Any, _T]:
        return await fn(*args,
                        **kwargs)

    return wrapper


async def fetch_data(session: ClientSession,
                     url: TPathLike):
    async with session.get(url) as response:
        return await response.json()


@run
async def fetch_content(url: TPathLike):
    async with ClientSession() as session:
        async with session.get(url) as response:
            return (
                response.headers.get("content-type"),
                await response.read()
            )


@run
async def download_file(url: TPathLike,
                        destination: TPathLike):
    async with ClientSession() as session:
        async with session.get(url) as response:
            if response.status != 200:
                return response.status

            async with aiofiles.open(destination, "wb") as f:
                async for data, _ in response.content.iter_chunks():
                    await f.write(data)
                    return response.status


def download(url: TPathLike,
             destination: TPathLike):
    with open(destination, "wb") as f:
        return f.write(requests.get(url).content)


async def main() -> None:
    loaded = YamlManager("config.yml")

    uri = load_uri(
        loaded["API"],
        loaded["CX"]
    )
    print(uri("Hello"))
    pass


def load():
    if not os.path.exists(r".\results.json"):
        return
    with open("results.json", "r") as f:
        results = json.load(f)
        return results


def unzip(file: TPathLike):
    with zipfile.ZipFile(file, mode="r") as z:
        z.extractall(r".\font_heap")


if __name__ == "__main__":
    asyncio.run(main())

    resp = load()
    items = resp[0]["items"]
    for item in items:
        page = requests.get(item["link"])

        soup = BeautifulSoup(page.content, "html.parser")
        result = soup.find(id="fulldownloadid")
        if result is None:
            continue
        href = result["href"]
        i: Any = fetch_content(href)
        isZip: bool = str(i[0]).find("zip") != -1
        path = f"./fonts/{search}"

        download(href, "font.zip")

# TODO missing download link
# TODO scrape the actual file from ffonts.net
# TODO store them in ./fonts/
# PROFIT
# TODO unzip the file,
#  pick the actual otf/something else file and move it out and delete the old zip file + unzipped folder
# TODO remodel this so it supports multiple entries and then executes them
