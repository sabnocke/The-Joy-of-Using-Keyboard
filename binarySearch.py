import bisect


class Solution:
    @staticmethod
    def search(self, nums: list[int], target: int) -> int:
        index = bisect.bisect_left(nums, target)
        return index if index < len(nums) and nums[index] == target else -1

    def search_2(self, arr: list[int], target: int, low: int, high: int) -> int:
        index = (low + high) // 2
        if arr[index] == target: return index
        if target > high or target < low: return -1
        if index > target:
            self.search_2(arr, target, low, index - 1)
        elif index < target:
            self.search_2(arr, target, index + 1, high)
        return -1


lst: list[int] = [3, 9, 5, 7, 8, 2, 11]
ln = len(lst)
sol = Solution()
lst.sort()
print(sol.search(nums=lst, target=7))  # 8.099999831756577e-06
print(sol.search_2(arr=lst, target=7, low=0, high=ln - 1))  # 1.8999999156221747e-06
