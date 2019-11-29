import java.lang.Math; 
import java.util.Random;

public class Perf {
    private static Random random = new Random();

    public static long Sum(long a, long b){
        return a + b + random.nextInt();
    }

    public static void main(String args[]) {  
        long sum = 0;
        for(long count = 0; count < Math.pow(10, 8); count++)
        {
            sum = Sum(sum, count);
        }
    }
}