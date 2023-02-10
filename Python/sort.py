class SortAlgo:
    """
    collection of sorting algorithms

    :arg lst: list to be sorted
    """

    def __init__(self, lst):
        self.lst = lst

    # swap
    def swap(self, first: int, second: int) -> None:
        """
        swaps values in list

        :param first: first index
        :param second: second index
        """
        self.lst[first], self.lst[second] = self.lst[second], self.lst[first]

    def __len__(self):
        """returns length of input list"""
        return len(self.lst)

    def __repr__(self):
        method_list = [method for method in dir(self) if method.startswith('__') is False and
                       method.endswith('sort') is True]
        return '\n'.join(method_list)

    def ascent(self, to_test) -> bool:
        """ tests if list is in ascending order

        :return: True if passes, else False;
        """
        return all(to_test[i] <= to_test[i + 1] for i in range(len(self) - 1))

    # bubble sort
    def bubble_sort(self) -> list:
        for _ in range(len(self) - 1):
            for i in range(len(self) - 1):
                if self.lst[i] > self.lst[i + 1]:
                    self.swap(i, i + 1)
        return self.lst

    # shaker sort
    def shaker_sort(self) -> list:
        start, end = 0, len(self) - 1
        swapped: bool = True
        while swapped:
            swapped = False
            for i in range(start, end):
                if self.lst[i] > self.lst[i + 1]:
                    self.swap(i, i + 1)
                    swapped = True

            if not swapped:
                break

            swapped = False
            end -= 1

            for a in range(end, start, -1):
                if self.lst[a] < self.lst[a - 1]:
                    self.swap(a, a - 1)
                    swapped = True
            if not swapped:
                break
            start += 1
        return self.lst

    # insert sort
    def insert_sort(self) -> list:
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

        def insSort():
            lng = len(self)
            for i in range(lng):
                j = i - 1
                selected = self.lst[i]
                loc = binary_search(self.lst, selected, 0, j)

                while j >= loc:
                    self.lst[j + 1] = self.lst[j]
                    j -= 1
                self.lst[j + 1] = selected
            return self.lst

        return insSort()

    # select sort
    def selection_sort(self) -> list:
        def selSort():
            loc = 0
            length: int = len(self)
            while loc < length - 1:
                j: int = locOfSmallest(self.lst, loc, length - 1)
                self.swap(loc, j)
                loc += 1
            return self.lst

        def locOfSmallest(flg_lst: list[int], start: int, end: int) -> int:
            i: int = start
            j: int = i
            while i <= end:
                if flg_lst[i] < flg_lst[j]:
                    j = i
                i += 1
            return j

        return selSort()

    # merge sort
    def merge_sort(self) -> list:

        # noinspection PyUnusedLocal
        def mrgSort(*args: int):
            if len(self) <= 1:
                return self.lst
            mid = len(self) // 2
            left = mrgSort(self.lst[:mid])
            right = mrgSort(self.lst[mid:])
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

        return mrgSort()

    # quick sort
    def quick_sort(self) -> list:
        start = 0
        end = len(self) - 1

        def partition(arr, low, high):
            mid = (low + high) // 2
            i_left, i_right = low, high
            pivot = arr[mid]
            while i_left < i_right:
                while arr[i_left] < pivot:
                    i_left += 1
                while arr[i_right] > pivot:
                    i_right -= 1
                if i_left <= i_right:
                    self.swap(i_left, i_right)
                    i_left += 1
                    i_right -= 1
            return i_left

        def qSorting(arr: list[int], low: int, high: int):
            mid_point = partition(arr, low, high)
            if low < high:
                qSorting(arr, low, mid_point - 1)
                qSorting(arr, mid_point, high)
            return arr

        return qSorting(self.lst, start, end)

    # comb sort
    def comb_sort(self) -> list:
        def getNextGap(gap):
            gap = (gap * 10) // 13
            return max(gap, 1)

        def cSort():
            n: int = len(self)
            gap = n
            swapped = True
            while gap != 1 and swapped == 1:
                gap = getNextGap(gap)
                swapped = False
                for i in range(n - gap):
                    if self.lst[i] > self.lst[i + gap]:
                        self.swap(i, i + gap)
                        swapped = True
            return self.lst

        return cSort()

    # pigeonhole sort
    def pigeonhole_sort(self):
        """
        Similar to Counting sort, although it should work with negative numbers
        :raise TypeError: input should be int
        :return: sorted arr
        """
        min_ = min(self.lst)
        max_ = max(self.lst)
        range_ = max_ - min_ + 1
        holes = [0] * range_
        for x in self.lst:
            if x is not int:
                raise TypeError("Should always be int")
            holes[x - min_] += 1
        i = 0
        for count in range(range_):
            while holes[count] > 0:
                holes[count] -= 1
                self.lst[i] = count + min_
                i += 1
        return self.lst


test: list[int] = [8, 4, 1, 56, 3, -44, 23, -6, 28, 0]
sort = SortAlgo(test)
print(sort)
