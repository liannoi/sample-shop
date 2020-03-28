'use strict';

function test() {
    const softDeleter = new SoftDeleter(document.querySelectorAll('.btn-cmd-delete'),
        '/Good/Delete',
        'Good successfully removed.');
    softDeleter.subscribeToDelete();
}

test();