#include <fstream>
#include <string.h>
using namespace std;
#define fi "KRUSKAL.INP" // file nhap
#define fo "KRUSKAL.OUT" // file xuat

const int MAXN = 1000;
const int MAXM = 1000;

struct Canh {
    int dinhdau, dinhcuoi;
    int dodai;
};

int n, m;// n la so canh, m la so dong trong file
Canh a[MAXM];
int father[MAXN];

fstream f;

void Nhap() {
    f.open(fi, ios::in);
    f >> n >> m;
    for (int i=1; i <= m; i++) {
		f >> a[i].dinhdau >> a[i].dinhcuoi >> a[i].dodai;
	}
    f.close();
}

void SapXep() {
    Canh tg;
    int i, j;
    for (i=1; i<=m-1; i++)
        for (j=i+1; j <= m; j++)
            if (a[i].dodai > a[j].dodai) {
                    tg = a[i];
                    a[i] = a[j];
                    a[j] = tg;
            }
}

int root(int x) {
    if (father[x] == -1) return x;
        else return root(father[x]);
}

void hopnhat(int r1, int r2) { /// r1 khac r2
    if (r1 < r2) father[r2] = r1;
        else /// r1 > r2
            father[r1] = r2;
}

void Kruskal() {
    SapXep();
    int i;
    for (i=1; i <= n; i++) father[i] = -1;
    int dem = 0;
    int tong = 0;
    int x, y;
    int r1, r2;
    f.open(fo, ios::out);
    for (i=1; i <= m; i++) {
        if (dem == n-1) break;
        x = a[i].dinhdau;
        y = a[i].dinhcuoi;

        r1 = root(x);
        r2 = root(y);

        if (r1 != r2) {
            f << x << "   " << y << endl; /// end line
            dem++;
            tong = tong + a[i].dodai;
            hopnhat(r1, r2);
        }
    }
    f << "Tong trong so cua cay khung be nhat la: " << tong;
    f.close();
}


int main() {
    Nhap();
    Kruskal();
}
