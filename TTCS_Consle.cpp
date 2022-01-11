#include <iostream>
using namespace std;


const int MAXN = 1000;
const int MAXM = 1000;

struct Canh {
    int dinhdau, dinhcuoi;
    int dodai;
};

int n, m;// n la so canh, m la so dong trong file
Canh a[MAXM];
int father[MAXN];


void Nhap() {
    cout << "Nhap so dinh: ";
    cin >> n;
    cout << "Nhap so canh: ";
    cin >> m;
    for (int i=1; i <= m; i++) {
		cin >> a[i].dinhdau >> a[i].dinhcuoi >> a[i].dodai;
	}
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
    int x, y, z;
    int r1, r2;
   
    for (i=1; i <= m; i++) {
        if (dem == n-1) break;
        x = a[i].dinhdau;
        y = a[i].dinhcuoi;
		z = a[i].dodai;
        r1 = root(x);
        r2 = root(y);

        if (r1 != r2) {
            cout << x << "	"<< y <<"	"<< z << endl; /// end line
            dem++;
            tong = tong + a[i].dodai;
            hopnhat(r1, r2);
        }
    }
    cout << "Tong cay khung nho nhat la: " << tong;
}


int main() {
    Nhap();
    Kruskal();
}
