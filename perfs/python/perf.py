import random
import math
import sys 

def Sum(a, b):
    return a + b + random.randint(0,1024)#sys.maxsize)

def main():
    sum = 0
    for count in range(0, 10**8):
        sum = Sum(sum, count)
    return sum

if __name__ == "__main__":
    main()