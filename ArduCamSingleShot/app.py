from flask import Flask, request

import requests

app = Flask(__name__)

@app.route('/upload/', methods=['POST'])
def upload():
    with open('capture.jpg', 'wb') as f:
        f.write(request.data)
    return 'OK, saved file'


@app.route('/upload/process/', methods=['POST'])
def upload():
    with open('capture.jpg', 'wb') as f:
        f.write(request.data)
    process_request = request('http://<ip-of-device>:5001/detect', files={'image': open('capture.jpg', 'rb') })
    process_request.raise_for_status()

    return 'OK, saved file'

if __name__ == '__main__':
    app.run(debug=True, host='0.0.0.0')
