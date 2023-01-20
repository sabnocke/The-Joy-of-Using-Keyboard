# swap
def swap(array: list[int], first: int, second: int) -> None:
    """

    :rtype: object
    :param array: array of ints
    :param first: first index
    :param second: second index
    """
    array[first], array[second] = array[second], array[first]


def test_ascent(arr) -> bool:
    """
    
    :param arr: list to test
    :return: True if passes, else False; Actually prints tho
    """
    return all(arr[i] <= arr[i + 1] for i in range(len(arr) - 1))


# bubble sort
def bubble_sort(array: list) -> list:
    for _ in range(len(array) - 1):
        for i in range(len(array) - 1):
            if array[i] > array[i + 1]:
                swap(array, i, i + 1)
    return array


# shaker sort
def shaker_sort(array: list[int]) -> list:
    start, end = 0, len(array) - 1
    swapped: bool = True
    while swapped:
        swapped = False
        for i in range(start, end):
            if array[i] > array[i + 1]:
                swap(array, i, i + 1)
                swapped = True

        if not swapped:
            break

        swapped = False
        end -= 1

        for a in range(end, start, -1):
            if array[a] < array[a - 1]:
                swap(array, a, a - 1)
                swapped = True
        if not swapped:
            break
        start += 1
    return array


# insert sort
def insert_sort(arr: list[int]) -> list:
    def binary_search(flg_lst: list[int], target: int, low: int, high: int):
        while low <= high:
            mid = low + (high - low) // 2
            if target == flg_lst[mid]:
                return mid + 1
            elif target > flg_lst[mid]:
                low = mid + 1
            else:
                high = mid - 1
        return low

    def insSort(flg_lst: list[int]):
        lng = len(flg_lst)
        for i in range(lng):
            j = i - 1
            selected = flg_lst[i]
            loc = binary_search(flg_lst, selected, 0, j)

            while j >= loc:
                flg_lst[j + 1] = flg_lst[j]
                j -= 1
            flg_lst[j + 1] = selected
        return flg_lst

    return insSort(arr)


# select sort
def selection_sort(arr: list[int]) -> list:
    def selSort(flg_lst: list[int]):
        loc = 0
        length: int = len(flg_lst)
        while loc < length - 1:
            j: int = locOfSmallest(flg_lst, loc, length - 1)
            swap(flg_lst, loc, j)
            loc += 1
        return arr

    def locOfSmallest(flg_lst: list[int], start: int, end: int) -> int:
        i: int = start
        j: int = i
        while i <= end:
            if flg_lst[i] < flg_lst[j]:
                j = i
            i += 1
        return j

    return selSort(arr)


# merge sort
def merge_sort(arr: list[int]) -> list:
    def mrgSort(flg_lst):
        if len(flg_lst) <= 1:
            return flg_lst
        mid = len(flg_lst) // 2
        left = mrgSort(flg_lst[:mid])
        right = mrgSort(flg_lst[mid:])
        return merge(left, right)

    def merge(left, right):
        result = []
        i = j = 0
        while i < len(left) and j < len(right):
            if left[i] <= right[j]:
                result.append(left[i])
                i += 1
            else:
                result.append(right[j])
                j += 1
        result += left[i:]
        result += right[j:]
        return result

    return mrgSort(arr)


# quick sort
def quick_sort(data: list) -> list:
    start = 0
    end = len(data) - 1

    def partition(arr, low, high):
        mid = (low + high) // 2
<<<<<<< Updated upstream
        i_left = low
        i_right = high
        pivot = arr[mid]
        while i_left < i_right:
            while arr[i_left] < pivot:
                i_left += 1
            while arr[i_right] > pivot:
                i_right -= 1
            if i_left <= i_right:
                swap(arr, i_left, i_right)
                i_left += 1
                i_right -= 1
        return i_left

    def qSort(arr: list[int], low: int, high: int):
        mid_point = partition(arr, low, high)
        if low < mid_point - 1:
            qSort(arr, low, mid_point - 1)
        if mid_point < high:
            qSort(arr, mid_point, high)
=======
        pivot = mid
        while low < high:
            while arr[low] < arr[pivot]:
                low += 1
            while arr[high] > arr[pivot]:
                high -= 1
            while low <= high:
                swap(arr, low, high)
                low += 1
                high -= 1
        print(low)
        return low

    def qSort(arr: list[int], low: int, high: int):
        mid_point = partition(arr, low, high)
        if  low < high:
            qSort(arr, low, mid_point - 1)
            qSort(arr, mid_point, high)
            print("high")
>>>>>>> Stashed changes
        return arr

    return qSort(data, start, end)


# comb sort
def comb_sort(arr: list[int]) -> list:
    def getNextGap(gap):
        gap = (gap * 10) // 13
        return max(gap, 1)

    def cSort(flg_lst):
        n = len(flg_lst)
        gap = n
        swapped = True
        while gap != 1 and swapped == 1:
            gap = getNextGap(gap)
            swapped = False
            for i in range(n - gap):
                if flg_lst[i] > arr[i + gap]:
                    swap(flg_lst, i, i + gap)
                    swapped = True
        return flg_lst

    return cSort(arr)


# pigeonhole sort
def pigeonhole_sort(arr: list):
    min_ = min(arr)
    max_ = max(arr)
    range_ = max_ - min_ + 1
    holes = [0] * range_
    for x in arr:
        assert type(x) is int, "int only!"
        holes[x - min_] += 1
    i = 0
    for count in range(range_):
        while holes[count] > 0:
            holes[count] -= 1
            arr[i] = count + min_
            i += 1
    return arr


lst = [8, 4, 1, 56, 3, -44, 23, -6, 28, 0]
lst2 = [10, 2, 5, 89, 1496, 256, 7, 89]
quick_sort(lst)
print(lst)
bubble_sort(lst)
print(lst)



