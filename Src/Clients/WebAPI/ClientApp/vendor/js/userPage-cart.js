'use strict';

async function request(id, count) {
    return await fetch(new Request('/Users/Update'),
        {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({ goodId: id, count: count })
        }).then(
        function(response) {
            if (response.status === 200) {
                window.location.href = '/Users/Show';
            }
        });
};

function runtimeForGoodsCounts() {
    document.querySelectorAll('#goodCountNumeric').forEach(e => e.addEventListener('focusout',
        function() {
            request(this.childNodes[1].dataset.id, this.childNodes[1].value);
        }));
}

function runtimeForGoodsDelete() {
    const softDeleter = new SoftDeleter(document.querySelectorAll('.btn-cmd-delete'),
        '/Users/Delete',
        'Product successfully removed from cart.',
        function() {
            window.location.href = '/Users/Show';
        });
    softDeleter.subscribeToDelete();
}

function runtime() {
    runtimeForGoodsCounts();
    runtimeForGoodsDelete();
}

runtime();