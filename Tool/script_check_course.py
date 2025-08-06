import requests
import threading
from concurrent.futures import ThreadPoolExecutor

# ==== CONFIG ====
CODES = [
    "BAO", "BAT", "CNS", "CTR", "CTX", "DTV", "HAN", "HOA", "KNM", "KQH", "KTE", "KTR", "LIS",
    "LLCTKT", "LLCTLS", "LLCTTH", "LLCTTT", "LLCTXH", "LUA", "MTH", "MTR", "NNH", "QLN", "SIK",
    "SIN", "TIN", "TLH", "TOA", "TOQ", "TRI", "VAN", "VLY", "XHH"
]

CHECK_URL = "https://student.husc.edu.vn/Studying/Courses/{code}/"  # THAY ĐỊA CHỈ URL NÀY
HEADERS = {
    "User-Agent": "Mozilla/5.0",
    "Cookie": "__RequestVerificationToken=3xBjfny2gn9pJ0z0cvCCVxmXfq1o1T4Yq-IvD3YA6PE9oZ1vagEqNWNofFLUlgLK4gvlhLffdphhLK2kn6NCQkiIUWQ1; ASP.NET_SessionId=5xqcwknqeucxwvaqzwbzthry; UMS.StudentPortal.F5C0A1C9384C2E25E79BA1ABF5D9A037=0434040F6C9248319FD3677C5553DDF92EEE28E3325DC62223C38AEBD7F9F72405ADF642515A7C51037E1AC2EDFE2A82D75240C71B9CEC803EC15815C9E2C9D6DFAEA803850692E7BACFDE0F0040FD03039D2C9312CE63D4818A36010BAE4F2A08321904E56E4B0800400B3C47998AE2D89D8184679C92A09479CF14195FF7D889575140E6D20D0C55D4E0E40A52E3B0C56F7BFACB164C6D9BEC10F2E4E3CBC207B31577D928580F15F5DFFB55232A6065B47AF0E5514A5EE978721381805E678A4A43C1961D08C50419FDD393FFC41FD0D12781CC6161A2E3069C34FE87601ACF2DB33F5939CE04E479E0E82A85BA1EC8097CB231C2176CA5A04C18C806EFA8A79696E6"  # THAY COOKIE THẬT VÀO ĐÂY
}

THREAD_COUNT = 50  # Số luồng song song
LOCK = threading.Lock()  # Để tránh xung đột khi ghi file

# ==== HÀM CHECK ====
def check_code(code, max_retries=99999):
    for attempt in range(max_retries):
        try:
            url = CHECK_URL.format(code=code)
            response = requests.get(url, headers=HEADERS)
            status = response.status_code
            if 'class="row form-group hitec-border-bottom-dotted"' in response.text:
                print(f"[{status}] {code} → ✅ HỢP LỆ")
                with LOCK:
                    with open("found_codes.txt", "a", encoding="utf-8") as f:
                        f.write(code + "\n")
            else:
                print(f"[{status}] {code} → ❌ KHÔNG HỢP LỆ")
            break
        except Exception as e:
            print(f"[LỖI] {code} → {e}")
            if attempt == max_retries - 1:
                print(f"[LỖI] {code} → Đã thử lại {max_retries} lần nhưng vẫn lỗi.")

# ==== TẠO DANH SÁCH MÃ ====
def generate_all_codes():
    all_codes = []
    for prefix in CODES:
        suffix_length = 7 - len(prefix)
        if suffix_length <= 0:
            continue

        start = 10 ** (suffix_length - 1)
        end = 10 ** suffix_length

        for i in range(start, end):
            full_code = f"{prefix}{i}"
            all_codes.append(full_code)

    return all_codes

# ==== MAIN ====
if __name__ == "__main__":
    print("🔍 BẮT ĐẦU KIỂM TRA TẤT CẢ CÁC MÃ...\n")

    all_codes = generate_all_codes()

    with ThreadPoolExecutor(max_workers=THREAD_COUNT) as executor:
        executor.map(check_code, all_codes)

    print("\n✅ HOÀN TẤT! Các mã hợp lệ đã lưu vào 'found_codes.txt'")
