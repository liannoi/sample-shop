'use strict';

function test() {
    const softDeleter = new SoftDeleter(document.querySelectorAll('.btn-cmd-delete'),
        '/Category/Delete',
        'Category successfully removed.');
    softDeleter.subscribeToDelete();
}

test();