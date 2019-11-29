#include <math.h>
#include <stdlib.h>

long Sum(long a, long b){
    return a + b + rand();
}

int main(void){
    long sum = 0;
    for (long count = 0; count < pow(10,8); count++)
    {
        sum = Sum(sum, count);
    }
    return (int)sum;
}